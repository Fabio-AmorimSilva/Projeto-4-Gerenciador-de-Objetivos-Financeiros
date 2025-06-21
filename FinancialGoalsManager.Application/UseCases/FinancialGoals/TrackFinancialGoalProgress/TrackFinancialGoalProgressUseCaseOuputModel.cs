namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.TrackFinancialGoalProgress;

public sealed record TrackFinancialGoalProgressUseCaseOuputModel
{
    public int Month { get; init; }
    public int Year { get; init; }
    public decimal Total { get; init; }
    public string FinancialGoalName { get; init; } = null!;
    public GoalStatus Status { get; init; }
}