namespace FinancialGoalsManager.Domain.Entities;

public sealed class FinancialGoal : SoftDeleteEntity
{
    public string Title { get; private set; } = null!;
    public decimal Goal { get; private set; }
    public DateTime DueDate { get; private set; }
    public decimal? MonthGoal { get; private set; }
    public GoalStatus Status { get; private set; }

    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public FinancialGoal(){}
    
    public FinancialGoal(
        string title,
        decimal goal,
        DateTime dueDate, 
        decimal? monthGoal,
        GoalStatus status
    )
    {
        Title = title;
        Goal = goal;
        DueDate = dueDate;
        MonthGoal = monthGoal;
        Status = status;
    }
}