namespace FinancialGoalsManager.Domain.Entities;

public sealed class Transaction : SoftDeleteEntity
{
    public decimal Quantity { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime Date { get; private set; }
    
    public Guid FinancialGoalId { get; private set; }
    public FinancialGoal FinancialGoal { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    
    public Transaction(){}
    
    public Transaction(
        decimal quantity,
        TransactionType type,
        DateTime date,
        FinancialGoal financialGoal,
        User user
    )
    {
        Quantity = quantity;
        Type = type;
        Date = date;
        
        FinancialGoal = financialGoal;
        FinancialGoalId = financialGoal.Id;
        
        UserId = User.Id;
        User = user;
    }
}