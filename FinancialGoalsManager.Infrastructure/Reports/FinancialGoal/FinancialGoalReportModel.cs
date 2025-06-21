namespace FinancialGoalsManager.Infrastructure.Reports.FinancialGoal;

public sealed record FinancialGoalReportModel
{
    public int Month { get; init; }
    public int Year { get; init; }
    public decimal Total { get; init; }
    public string FinancialGoalName { get; init; } = null!;
    public GoalStatus Status { get; init; }
}