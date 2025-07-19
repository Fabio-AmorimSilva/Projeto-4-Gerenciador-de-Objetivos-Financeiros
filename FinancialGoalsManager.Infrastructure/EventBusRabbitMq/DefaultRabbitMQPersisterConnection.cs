namespace FinancialGoalsManager.Infrastructure.EventBusRabbitMq;

public sealed class DefaultRabbitMqPersisterConnection(
    IConnectionFactory connectionFactory,
    ILogger<DefaultRabbitMqPersisterConnection> logger
) : IRabbitMqPersisterConnection
{
    private readonly IConnectionFactory _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    private readonly ILogger<DefaultRabbitMqPersisterConnection> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private IConnection _connection;
    private bool _disposed;

    private readonly Lock _syncRoot = new();

    public bool IsConnected => _connection.IsOpen && !_disposed;

    public async Task<IChannel> CreateModel()
    {
        if (!IsConnected)
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

        return await _connection.CreateChannelAsync();
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;

        try
        {
            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex.ToString());
        }
    }

    public Task<bool> TryConnect()
    {
        _logger.LogInformation("RabbitMQ Client is trying to connect");

        lock (_syncRoot)
        {
            var policy = Policy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, _) => { _logger.LogWarning(ex.ToString()); });

            policy.Execute(async () => { _connection = await _connectionFactory.CreateConnectionAsync(); });

            if (IsConnected)
            {
                _connection.ConnectionShutdownAsync += OnConnectionShutdown;
                _connection.CallbackExceptionAsync += OnCallbackException;
                _connection.ConnectionBlockedAsync += OnConnectionBlocked;

                _logger.LogInformation(
                    $"RabbitMQ persister connection acquire a connection {_connection.Endpoint.HostName} and is subscribed to failure events");

                return Task.FromResult(true);
            }

            _logger.LogCritical("FATAL ERROR: RabbitMQ connections can't be created and opened");

            return Task.FromResult(false);
        }
    }

    private Task OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return Task.CompletedTask;

        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

        TryConnect();
        return Task.CompletedTask;
    }

    private Task OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return Task.CompletedTask;

        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

        TryConnect();
        return Task.CompletedTask;
    }

    private Task OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (_disposed)
            return Task.CompletedTask;

        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

        TryConnect();
        return Task.CompletedTask;
    }
}