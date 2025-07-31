namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record FinancialGoalUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid FinancialGoalId { get; set; }
    public string Title { get; set; }

    public FinancialGoalUpdatedIntegrationEvent(
        Guid financialGoalId,
        string title
    )
    {
        FinancialGoalId = financialGoalId;
        Title = title;
    }
}