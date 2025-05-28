namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.AddFinancialGoal;

public sealed record AddFinancialGoalInputModel
{
    public string Title { get; init; } = null!;
    public decimal Goal { get; init; }
    public DateTime DueDate { get; init; }
    public decimal? MonthGoal { get; init; }
    public GoalStatus Status { get; init; }
}