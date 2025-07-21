namespace FinancialGoalsManager.Domain.EventBus;

public interface IEventBus
{
    Task<ValueTask> PublishAsync(IntegrationEvent @event);
}