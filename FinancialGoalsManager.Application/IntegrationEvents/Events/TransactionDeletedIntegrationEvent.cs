namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record TransactionDeletedIntegrationEvent : IntegrationEvent
{
    public Guid TransactionId { get; set; }
    
    public TransactionDeletedIntegrationEvent(
        Guid id, 
        DateTime createdAt,
        Guid transactionId
    ) : base(id, createdAt)
    {
        TransactionId = transactionId;
    }
}