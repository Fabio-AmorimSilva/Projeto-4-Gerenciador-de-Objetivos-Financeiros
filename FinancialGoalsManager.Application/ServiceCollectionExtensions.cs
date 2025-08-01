﻿namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<ICreateFinancialGoalUseCase, CreateFinancialGoalUseCase>();
        services.AddScoped<IUpdateFinancialGoalUseCase, UpdateFinancialGoalUseCase>();
        services.AddScoped<IDeleteFinancialGoalUseCase, DeleteFinancialGoalUseCase>();
        services.AddScoped<IGetFinancialGoalUseCase, GetFinancialGoalUseCase>();
        services.AddScoped<IListFinancialGoalsUseCase, ListFinancialGoalsUseCase>();
        services.AddScoped<ISimulateFinancialGoalProgressUseCase, SimulateFinancialGoalProgressUseCase>();
        services.AddScoped<ITrackFinancialGoalProgress, TrackFinancialGoalProgressUseCase>();
        services.AddScoped<ITrackFinancialGoalsReportUseCase, TrackFinancialGoalsReportUseCase>();
        services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
        services.AddScoped<IDeleteTransactionUseCase, DeleteTransactionUseCase>();
        services.AddScoped<IGetTransactionUseCase, GetTransactionUseCase>();
        services.AddScoped<IListTransactionUseCase, ListTransactionsUseCase>();
        services.AddTransient<TransactionCreatedIntegrationEventHandler>();
        services.AddTransient<TransactionDeletedIntegrationEventHandler>();
        services.AddTransient<FinancialGoalCreatedIntegrationEventHandler>();
        services.AddTransient<FinancialGoalDeletedIntegrationEventHandler>();
        services.AddTransient<FinancialGoalUpdatedIntegrationEventHandler>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}