namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureEventBusHandlers(this IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

        eventBus.Subscribe<TransactionCreatedIntegrationEvent, TransactionCreatedIntegrationEventHandler>();
        eventBus.Subscribe<TransactionDeletedIntegrationEvent, TransactionDeletedIntegrationEventHandler>();
        eventBus.Subscribe<FinancialGoalCreatedIntegrationEvent, FinancialGoalCreatedIntegrationEventHandler>();
        eventBus.Subscribe<FinancialGoalDeletedIntegrationEvent, FinancialGoalDeletedIntegrationEventHandler>();
        eventBus.Subscribe<FinancialGoalUpdatedIntegrationEvent, FinancialGoalUpdatedIntegrationEventHandler>();
    }
}