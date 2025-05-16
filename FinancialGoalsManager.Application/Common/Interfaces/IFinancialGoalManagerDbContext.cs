namespace FinancialGoalsManager.Application.Common.Interfaces;

public interface IFinancialGoalManagerDbContext
{
    DbSet<FinancialGoal> FinancialGoals { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}