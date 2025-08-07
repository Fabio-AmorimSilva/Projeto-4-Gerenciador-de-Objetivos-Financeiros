namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record FinancialGoalDeletedIntegrationEvent : IntegrationEvent
{
    public Guid FinancialGoalId { get; set; }

    public FinancialGoalDeletedIntegrationEvent(
        Guid financialGoalId
    )
    {
        FinancialGoalId = financialGoalId;
    }
}