namespace FinancialGoalsManager.Infrastructure.Persistence;

public sealed class FinancialGoalManagerDbContext(
    DbContextOptions<FinancialGoalManagerDbContext> options
) : DbContext(options),
    IFinancialGoalManagerDbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyGlobalSoftEntityDeleteFilter<SoftDeleteEntity>(
            modelBuilder: modelBuilder,
            expression: e => !e.IsDeleted
        );
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(optionsBuilder);
    }

    private static void ApplyGlobalSoftEntityDeleteFilter<TEntity>(ModelBuilder modelBuilder, Expression<Func<TEntity, bool>> expression)
    {
        var entities = GetEntitiesFromBaseType<TEntity>(modelBuilder);
        foreach (var entity in entities)
        {
            var param = Expression.Parameter(entity);
            var body = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), param, expression.Body);
            modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(body, param));
        }
    }
    
    private static IEnumerable<Type> GetEntitiesFromBaseType<TBase>(ModelBuilder modelBuilder) => modelBuilder.Model
        .GetEntityTypes()
        .Where(t => t.BaseType == null)
        .Select(t => t.ClrType)
        .Where(t => typeof(TBase).IsAssignableFrom(t));
}