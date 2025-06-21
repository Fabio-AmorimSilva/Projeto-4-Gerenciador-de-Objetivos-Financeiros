namespace FinancialGoalsManager.Domain.Entities.Common;

public abstract class SoftDeleteEntity : AuditableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeleteAt { get; set; }
}