namespace FinancialGoalsManager.Domain.EventBus;

public interface IIntegrationEventHandler<in TEvent>
    where TEvent : IntegrationEvent
{
    Task HandleAsync(TEvent @event);
}