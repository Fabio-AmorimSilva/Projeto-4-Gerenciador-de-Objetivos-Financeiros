namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddUserUseCase, AddUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IAddFinancialGoalUseCase, AddFinancialGoalUseCase>(); 
        services.AddScoped<IUpdateFinancialGoalUseCase, UpdateFinancialGoalUseCase>();
        services.AddScoped<IDeleteFinancialGoalUseCase, DeleteFinancialGoalUseCase>();
        services.AddScoped<IGetFinancialGoalUseCase, GetFinancialGoalUseCase>();
        services.AddScoped<IListFinancialGoalsUseCase, ListFinancialGoalsUseCase>();
        services.AddScoped<IAddTransactionUseCase, AddTransactionUseCase>();
        services.AddScoped<IDeleteTransactionUseCase, DeleteTransactionUseCase>();
        services.AddScoped<IGetTransactionUseCase, GetTransactionUseCase>();
        services.AddScoped<IListTransactionUseCase, ListTransactionsUseCase>();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}