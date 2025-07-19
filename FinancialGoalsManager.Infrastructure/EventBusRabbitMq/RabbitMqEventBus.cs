namespace FinancialGoalsManager.Infrastructure.EventBusRabbitMq;

public sealed class RabbitMqEventBus(
    ILogger<RabbitMqEventBus> logger,
    IServiceProvider serviceProvider,
    IOptions<EventBusOptions> options,
    IOptions<EventBusSubscriptionInfo> subscriptionInfo
) : IEventBus, IDisposable, IHostedService
{
    private const string ExchangeName = "financialManager_event_bus";

    private readonly ResiliencePipeline _pipeline = CreateResiliencePipeline(options.Value.RetryCount);
    private readonly string _queue = options.Value.SubscriptionClientName;
    private readonly EventBusSubscriptionInfo _subscriptionInfo = subscriptionInfo.Value;
    private IConnection _rabbitMqConnection = null!;

    private IChannel _consumerChannel = null!;

    public async Task<ValueTask> PublishAsync(IntegrationEvent @event)
    {
        var routingKey = @event.GetType().Name;

        if (logger.IsEnabled(LogLevel.Trace))
            logger.LogTrace($"Creating RabbitMQ channel to publish event: {@event.Id} ({routingKey})");

        await using var channel = await _rabbitMqConnection.CreateChannelAsync() ?? throw new NullReferenceException("RabbitMQ connection is not open");

        if (logger.IsEnabled(LogLevel.Trace))
            logger.LogTrace($"Declaring RabbitMq exchange to publish event: {@event.Id}");

        await channel.ExchangeDeclareAsync(exchange: ExchangeName, type: ExchangeType.Direct);

        var body = SerializeMessage(@event);

        return _pipeline.ExecuteAsync(async async =>
        {
            var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent
            };

            if (logger.IsEnabled(LogLevel.Trace))
                logger.LogTrace($"Publishing event to RabbitMQ: {@event.Id}");

            try
            {
                await channel.BasicPublishAsync(
                    exchange: ExchangeName,
                    routingKey: routingKey,
                    mandatory: true,
                    basicProperties: properties,
                    body: body,
                    cancellationToken: async
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        });
    }

    private static ResiliencePipeline CreateResiliencePipeline(int retryCount)
    {
        var retryOptions = new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder().Handle<BrokerUnreachableException>().Handle<SocketException>(),
            MaxRetryAttempts = retryCount,
            DelayGenerator = context => ValueTask.FromResult(GenerateDelay(context.AttemptNumber))
        };

        return new ResiliencePipelineBuilder()
            .AddRetry(retryOptions)
            .Build();

        static TimeSpan? GenerateDelay(int attempt)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, attempt));
        }
    }

    private byte[] SerializeMessage(IntegrationEvent @event)
    {
        return JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), _subscriptionInfo.JsonSerializerOptions);
    }

    public void Dispose()
    {
        _consumerChannel?.Dispose();
    }

    private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        static IEnumerable<string> ExtractTraceContextFromBasicProperties(IBasicProperties props, string key)
        {
            if (props.Headers.TryGetValue(key, out var value))
            {
                var bytes = value as byte[];
                return [Encoding.UTF8.GetString(bytes)];
            }

            return [];
        }

        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            if (message.Contains("throw-fake-exception", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
            }

            await ProcessEvent(eventName, message);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);
        }

        await _consumerChannel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);
        }

        await using var scope = serviceProvider.CreateAsyncScope();

        if (!_subscriptionInfo.EventTypes.TryGetValue(eventName, out var eventType))
        {
            logger.LogWarning("Unable to resolve event type for event name {EventName}", eventName);
            return;
        }

        var integrationEvent = DeserializeMessage(message, eventType);

        foreach (var handler in scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(eventType))
        {
            await handler.Handle(integrationEvent);
        }
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode",
        Justification =
            "The 'JsonSerializer.IsReflectionEnabledByDefault' feature switch, which is set to false by default for trimmed .NET apps, ensures the JsonSerializer doesn't use Reflection.")]
    [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "See above.")]
    private IntegrationEvent DeserializeMessage(string message, Type eventType)
    {
        return JsonSerializer.Deserialize(message, eventType, _subscriptionInfo.JsonSerializerOptions) as IntegrationEvent;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    logger.LogInformation("Starting RabbitMQ connection on a background thread");

                    _rabbitMqConnection = serviceProvider.GetRequiredService<IConnection>();
                    if (!_rabbitMqConnection.IsOpen)
                    {
                        return;
                    }

                    if (logger.IsEnabled(LogLevel.Trace))
                    {
                        logger.LogTrace("Creating RabbitMQ consumer channel");
                    }

                    _consumerChannel = await _rabbitMqConnection.CreateChannelAsync(cancellationToken: cancellationToken);

                    _consumerChannel.CallbackExceptionAsync += async (sender, ea) =>
                    {
                        logger.LogWarning(ea.Exception, "Error with RabbitMQ consumer channel");
                    };

                    await _consumerChannel.ExchangeDeclareAsync(
                        exchange: ExchangeName,
                        type: "direct",
                        cancellationToken: cancellationToken
                    );

                    await _consumerChannel.QueueDeclareAsync(
                        queue: _queue,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null,
                        cancellationToken: cancellationToken
                    );

                    if (logger.IsEnabled(LogLevel.Trace))
                    {
                        logger.LogTrace("Starting RabbitMQ basic consume");
                    }

                    var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                    consumer.ReceivedAsync += OnMessageReceived;

                    await _consumerChannel.BasicConsumeAsync(
                        queue: _queue,
                        autoAck: false,
                        consumer: consumer,
                        cancellationToken: cancellationToken
                    );

                    foreach (var (eventName, _) in _subscriptionInfo.EventTypes)
                    {
                        await _consumerChannel.QueueBindAsync(
                            queue: _queue,
                            exchange: ExchangeName,
                            routingKey: eventName,
                            cancellationToken: cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error starting RabbitMQ connection");
                }
            },
            TaskCreationOptions.LongRunning);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}