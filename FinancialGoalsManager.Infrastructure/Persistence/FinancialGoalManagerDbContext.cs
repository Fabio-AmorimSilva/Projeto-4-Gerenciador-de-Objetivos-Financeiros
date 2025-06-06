namespace FinancialGoalsManager.Infrastructure.Persistence;

public sealed class FinancialGoalManagerDbContext(
    DbContextOptions<FinancialGoalManagerDbContext> options
) : DbContext(options),
    IFinancialGoalManagerDbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(optionsBuilder);
    }
}