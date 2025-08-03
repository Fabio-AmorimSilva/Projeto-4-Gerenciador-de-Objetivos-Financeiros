namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record TransactionDeletedIntegrationEvent : IntegrationEvent
{
    public Guid FinancialGoalId { get; set; }
    public Guid TransactionId { get; set; }
    
    public TransactionDeletedIntegrationEvent(
        Guid financialGoalId,
        Guid transactionId
    ) 
    { 
        FinancialGoalId = financialGoalId;
        TransactionId = transactionId;
    }
}