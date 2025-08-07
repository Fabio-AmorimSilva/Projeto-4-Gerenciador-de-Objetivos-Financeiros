namespace Microsoft.AspNetCore.Builder;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddUserProvider(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        return builder;
    }

    public static WebApplicationBuilder AddRabbitMq(
        this WebApplicationBuilder builder,
        IConfiguration configuration
    )
    {
        var section = configuration.GetSection("BrokerSettings");
        builder.Services.Configure<BrokerSettings>(section);
        var brokerSettings = section.Get<BrokerSettings>();
        
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            AddRabbitMqPersistentConnection(containerBuilder, brokerSettings);
            AddEventBusRabbitMq(
                containerBuilder,
                brokerName: brokerSettings.BrokerName,
                queueName: brokerSettings.QueueName
            );
        });

        return builder;
    }

    private static void AddRabbitMqPersistentConnection(
        ContainerBuilder builder,
        BrokerSettings brokerSettings
    )
    {
        builder.Register((container, p) =>
        {
            var logger = container.Resolve<ILogger<RabbitMqPersistentConnection>>();

            var factory = new ConnectionFactory
            {
                HostName = brokerSettings.Host,
                Uri = new Uri(brokerSettings.Uri),
                Port = brokerSettings.Port,
                UserName = brokerSettings.UserName,
                Password = brokerSettings.Password,
                DispatchConsumersAsync = true
            };

            return new RabbitMqPersistentConnection(factory, logger);
        }).As<IPersistentConnection>().SingleInstance();
    }

    private static void AddEventBusRabbitMq(
        ContainerBuilder builder,
        string brokerName,
        string queueName
    )
    {
        builder.RegisterType<InMemoryEventBusSubscriptionManager>()
            .As<IEventBusSubscriptionManager>()
            .SingleInstance();

        builder.Register(container =>
        {
            var persistentConnection = container.Resolve<IPersistentConnection>();
            var scope = container.Resolve<ILifetimeScope>();
            var subscriptionManager = container.Resolve<IEventBusSubscriptionManager>();
            var logger = container.Resolve<ILogger<EventBusRabbitMq>>();

            return new EventBusRabbitMq(
                persistentConnection,
                subscriptionManager,
                serviceProvider: scope,
                brokerName,
                queueName,
                logger
            );
        }).As<IEventBus>().SingleInstance();
    }
}