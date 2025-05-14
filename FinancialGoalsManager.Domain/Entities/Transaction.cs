namespace FinancialGoalsManager.Domain.Entities;

public sealed class Transaction : SoftDeleteEntity
{
    public decimal Quantity { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime Date { get; private set; }

    public Transaction(){}
    
    public Transaction(
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