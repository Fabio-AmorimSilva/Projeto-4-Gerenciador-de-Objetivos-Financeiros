namespace FinancialGoalsManager.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        AuditCreateInfo(eventData.Context);
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = new CancellationToken())
    {
        AuditCreateInfo(eventData.Context);
        return base.SavedChangesAsync(eventData, result, cancellationToken);
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