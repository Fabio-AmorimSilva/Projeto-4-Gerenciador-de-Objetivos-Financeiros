namespace FinancialGoalsManager.Application.IntegrationEvents.Events;

public record FinancialGoalCreatedIntegrationEvent : IntegrationEvent
{
    public string Title { get; init; } = null!;
    public decimal Goal { get; init; }
    public DateTime DueDate { get; init; }
    public decimal? MonthGoal { get; init; }

    public FinancialGoalCreatedIntegrationEvent(
        string title,
        decimal goal,
        DateTime dueDate, 
        decimal? monthGoal
    )
    {
        Title = title;
        Goal = goal;
        DueDate = dueDate;
        MonthGoal = monthGoal;
    }
}