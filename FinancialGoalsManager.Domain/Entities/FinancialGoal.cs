namespace FinancialGoalsManager.Domain.Entities;

public sealed class FinancialGoal : SoftDeleteEntity
{
    public const int TitleMaxLength = 200; 
    
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
        Guard.IsNotEmpty(title);
        Guard.IsLessThanOrEqualTo(title.Length, TitleMaxLength, nameof(title));
        Guard.IsNotDefault(goal);
        Guard.IsNotDefault(dueDate);
        
        Title = title;
        Goal = goal;
        DueDate = dueDate;
        MonthGoal = monthGoal;
        Status = status;
    }
    
    public void Update(
        string title,
        decimal goal,
        DateTime dueDate, 
        decimal? monthGoal,
        GoalStatus status
    )
    {
        Guard.IsNotEmpty(title);
        Guard.IsLessThanOrEqualTo(title.Length, TitleMaxLength, nameof(title));
        Guard.IsNotDefault(goal);
        Guard.IsNotDefault(dueDate);
        
        Title = title;
        Goal = goal;
        DueDate = dueDate;
        MonthGoal = monthGoal;
        Status = status;
    }
}