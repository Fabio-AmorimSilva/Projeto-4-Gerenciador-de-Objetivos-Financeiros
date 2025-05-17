namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<FinancialGoalManagerDbContext>(
            options => options.UseSqlServer(connectionString: connectionString)
        );

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<IFinancialGoalManagerDbContext>(provider => provider.GetRequiredService<FinancialGoalManagerDbContext>());
        
        return services;
    }
}