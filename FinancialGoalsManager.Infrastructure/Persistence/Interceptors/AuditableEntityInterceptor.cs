namespace FinancialGoalsManager.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AuditCreateInfo(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        AuditCreateInfo(eventData.Context); 
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private void AuditCreateInfo(DbContext? context)
    {
        if (context is null)
            return;
        
        var entities = context.ChangeTracker
            .Entries<AuditableEntity>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entity in entities)
        {
            entity.Entity.CreatedAt = DateTime.Now;
        }    
    }
}