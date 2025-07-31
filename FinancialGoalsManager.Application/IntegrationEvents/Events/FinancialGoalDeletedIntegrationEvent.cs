namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record FinancialGoalDeletedIntegrationEvent : IntegrationEvent
{
    public Guid FinancialGoalId { get; set; }
    public string Title { get; set; }

    public FinancialGoalDeletedIntegrationEvent(
        Guid financialGoalId,
        string title
    )
    {
        FinancialGoalId = financialGoalId;
        Title = title;
    }
}