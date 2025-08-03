namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record TransactionCreatedIntegrationEvent : IntegrationEvent
{
    public Guid FinancialGoalId { get; set; }
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }

    public TransactionCreatedIntegrationEvent(
        Guid financialGoalId,
        decimal quantity,
        TransactionType type,
        DateTime date
    )
    {
        FinancialGoalId = financialGoalId;
        Quantity = quantity;
        Type = type;
        Date = date;
    }
}