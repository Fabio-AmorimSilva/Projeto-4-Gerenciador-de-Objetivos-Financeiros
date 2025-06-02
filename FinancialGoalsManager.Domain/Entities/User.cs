namespace FinancialGoalsManager.Domain.Entities;

public class User : SoftDeleteEntity
{
    public const int MaxNameLength = 150;

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    
    private readonly List<FinancialGoal> _financialGoals = [];
    public IReadOnlyCollection<FinancialGoal> FinancialGoals => _financialGoals;
    
    private readonly List<Transaction> _transactions = [];
    public IReadOnlyCollection<Transaction> Transactions => _transactions;
    
    protected User(){}
    
    public User(
        string name,
        string email,
        string password
    )
    {
        Guard.IsNotWhiteSpace(name);
        Guard.IsLessThanOrEqualTo(name.Length, MaxNameLength, nameof(name));
        Guard.IsNotWhiteSpace(email);
        Guard.IsNotWhiteSpace(password);

        Name = name;
        Email = email;
        Password = password;
    }

    public void Update(
        string name,
        string email
    )
    {
        Guard.IsNotWhiteSpace(name);
        Guard.IsLessThanOrEqualTo(name.Length, MaxNameLength, nameof(name));
        Guard.IsNotWhiteSpace(email);

        Name = name;
        Email = email;
    }

    public void AddGoal(FinancialGoal financialGoal)
    {
        _financialGoals.Add(financialGoal);
    }

    public UseCaseResult<FinancialGoal> UpdateGoal(
        Guid financialGoalId,
        string title,
        decimal goal,
        DateTime dueDate, 
        decimal? monthGoal,
        GoalStatus status
    )
    {
        var financialGoal = GetGoal(financialGoalId);
        if (financialGoal is null)
            return UseCaseResult<FinancialGoal>.Error(ErrorMessages.NotFound<FinancialGoal>());
        
        financialGoal.Update(
            title: title,
            goal: goal,
            dueDate: dueDate,
            monthGoal: monthGoal,
            status: status
        );
        
        return UseCaseResult<FinancialGoal>.Success(financialGoal);
    }

    public FinancialGoal? GetGoal(Guid financialGoalId)
    {
        var financialGoal = _financialGoals.FirstOrDefault(f => f.Id == financialGoalId);

        return financialGoal;
    }

    public void DeleteGoal(FinancialGoal financialGoal)
    {
        _financialGoals.Remove(financialGoal);
    }
    
    public void AddTransaction(Transaction transaction)
    {
        if(transaction.Quantity < 0)
            throw new ArgumentOutOfRangeException(ErrorMessages.HasToBeGreaterThan(nameof(transaction.Quantity), 0));
        
        if(transaction.Date.Date <= DateTime.Today)
            throw new ArgumentOutOfRangeException(ErrorMessages.HasToBeGreaterThan(nameof(transaction.Date), DateTime.Today.Day));
            
        var financialGoal = GetGoal(transaction.FinancialGoalId);
        
        financialGoal.AddTotal(transaction.Quantity);
        
        _transactions.Add(transaction);
    }

    public Transaction? GetTransaction(Guid transactionId)
    {
        return _transactions.FirstOrDefault(t => t.Id == transactionId);
    }
    
    public void DeleteTransaction(Transaction transaction)
    {
        _transactions.Remove(transaction);
    }
    
    public void UpdatePassword(string password)
    {
        Guard.IsNotWhiteSpace(password);
        
        Password = password;
    }
}