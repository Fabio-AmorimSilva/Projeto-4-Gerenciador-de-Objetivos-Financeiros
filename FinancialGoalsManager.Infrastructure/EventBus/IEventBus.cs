namespace FinancialGoalsManager.Infrastructure.EventBus;

public interface IEventBus
{
    Task<ValueTask> PublishAsync(IntegrationEvent @event);
}