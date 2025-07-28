namespace FinancialGoalsManager.Domain.EventBus;

public interface IEventBus
{
    void Publish<TEvent>(TEvent @event)
        where TEvent : IntegrationEvent;

    void Subscribe<TEvent, TEventHandler>()
        where TEvent : IntegrationEvent
        where TEventHandler : IIntegrationEventHandler<TEvent>;

    void Unsubscribe<TEvent, TEventHandler>()
        where TEvent :  IntegrationEvent
        where TEventHandler : IIntegrationEventHandler<TEvent>;
}