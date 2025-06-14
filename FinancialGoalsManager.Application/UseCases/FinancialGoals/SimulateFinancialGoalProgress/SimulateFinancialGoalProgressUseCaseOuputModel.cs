namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.SimulateFinancialGoalProgress;

public sealed record SimulateFinancialGoalProgressUseCaseOuputModel
{
    public int Month { get; init; }
    public int Year { get; init; }
    public decimal Total { get; init; }
}