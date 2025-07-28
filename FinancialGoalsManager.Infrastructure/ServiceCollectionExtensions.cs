using FinancialGoalsManager.Application.IntegrationEvents.EventHandling;
using FinancialGoalsManager.Infrastructure.Notifications;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<FinancialGoalManagerDbContext>(options => options.UseSqlServer(connectionString: connectionString)
        );

        services.AddScoped<IFinancialGoalManagerDbContext>(provider => provider.GetRequiredService<FinancialGoalManagerDbContext>());
        services.AddJwtConfig(configuration);
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IFinancialGoalReportService, FinancialGoalReportService>();
        var eventBusSettings = configuration.GetSection("EventBusConnection");
        services.Configure<EventBusSettings>(eventBusSettings);
        var eventBusConnection = eventBusSettings.Get<EventBusSettings>();
        
        services.AddRabbitMq
        (
            connectionUrl: eventBusConnection.EventBusConnection,
            brokerName: "financialGoalsManagerBroker",
            queueName: "financialGoalsBusQueue",
            timeoutBeforeReconnecting: 15
        );
        var settings = configuration.GetSection("MailSettings");
        services.Configure<MailSettings>(settings);
        services.AddSingleton<IMailService, MailHandlingService>();
        
        return services;
    }

    private static void AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(settings);

        var appSettings = settings.Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings?.JwtKey!);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings?.ValidOn,
                ValidIssuer = appSettings?.Emissary
            };
        });
    }

    private static void AddRabbitMq(
        this IServiceCollection services,
        string connectionUrl,
        string brokerName, 
        string queueName,
        int timeoutBeforeReconnecting = 15
    )
    {
        services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();
        services.AddSingleton<IPersistentConnection, RabbitMQPersistentConnection>(factory =>
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(connectionUrl),
                DispatchConsumersAsync = true,
            };

            var logger = factory.GetService<ILogger<RabbitMQPersistentConnection>>();
            return new RabbitMQPersistentConnection(connectionFactory, logger, timeoutBeforeReconnecting);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMq>(factory =>
        {
            var persistentConnection = factory.GetService<IPersistentConnection>();
            var subscriptionManager = factory.GetService<IEventBusSubscriptionManager>();
            var logger = factory.GetService<ILogger<EventBusRabbitMq>>();

            return new EventBusRabbitMq(persistentConnection, subscriptionManager, factory, logger, brokerName, queueName);
        });
    }
}