namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record TransactionCreatedIntegrationEvent : IntegrationEvent
{
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }

    public TransactionCreatedIntegrationEvent(
        Guid id,
        DateTime createdAt,
        decimal quantity,
        TransactionType type,
        DateTime date
    ) : base(id, createdAt)
    {
        Quantity = quantity;
        Type = type;
        Date = date;
    }
}