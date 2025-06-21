namespace FinancialGoalsManager.Infrastructure.Persistence.Interceptors;

public sealed class SoftDeleteEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AuditSoftDeleteInfo(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        AuditSoftDeleteInfo(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AuditSoftDeleteInfo(DbContext? context)
    {
        if (context is null)
            return;

        var entities = context.ChangeTracker
            .Entries<SoftDeleteEntity>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entity in entities)
        {
            entity.State = EntityState.Modified;
            entity.Entity.IsDeleted = true;
            entity.Entity.DeleteAt = DateTime.Now;
        }
    }
}