namespace FinancialGoalsManager.Infrastructure.EventBus;

public interface IEventBus
{
    Task Publish(IntegrationEvent @event);
}