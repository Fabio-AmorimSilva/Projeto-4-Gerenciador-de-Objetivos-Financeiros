namespace FinancialGoalsManager.Application.Common.Interfaces;

public interface IFinancialGoalManagerDbContext
{
    DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}