namespace FinancialGoalsManager.Domain.Entities.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; set; }
}