namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddMvc(options => options.Filters.Add<ModelStateValidatorFilter>());
        services.AddHttpContextAccessor();
        services.AddScoped<IRequestContextService, RequestContextService>();

        return services;
    }
}