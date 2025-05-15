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

    public void AddTransaction(Transaction transaction)
    {
        if(transaction.Quantity < 0)
            throw new ArgumentOutOfRangeException(ErrorMessages.HasToBeGreaterThan(nameof(transaction.Quantity), 0));
        
        if(transaction.Date.Date <= DateTime.Today)
            throw new ArgumentOutOfRangeException(ErrorMessages.HasToBeGreaterThan(nameof(transaction.Date), DateTime.Today.Day));
            
        _transactions.Add(transaction);
    }

    public Transaction? GetTransaction(Guid transactionId)
    {
        return _transactions.FirstOrDefault(t => t.Id == transactionId);
    }
    
    public void RemoveTransaction(Guid transactionId)
    {
        var transaction = GetTransaction(transactionId);

        if (transaction is null)
            throw new NotFoundException(ErrorMessages.NotFound<Transaction>());
        
        _transactions.Remove(transaction);
    }
}