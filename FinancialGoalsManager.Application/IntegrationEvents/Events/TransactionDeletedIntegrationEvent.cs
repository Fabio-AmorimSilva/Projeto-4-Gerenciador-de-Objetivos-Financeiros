namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record TransactionDeletedIntegrationEvent : IntegrationEvent
{
    public Guid TransactionId { get; set; }
    
    public TransactionDeletedIntegrationEvent(
        Guid transactionId
    ) 
    {
        TransactionId = transactionId;
    }
}