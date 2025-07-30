namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record TransactionCreatedIntegrationEvent : IntegrationEvent
{
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }

    public TransactionCreatedIntegrationEvent(
        decimal quantity,
        TransactionType type,
        DateTime date
    )
    {
        Quantity = quantity;
        Type = type;
        Date = date;
    }
}