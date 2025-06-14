namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.SimulateFinancialGoalProgress;

public sealed record SimulateFinancialGoalProgressUseCaseInputModel
{
    public decimal Capital { get; init; }
    public decimal Fee { get; init; }
    public int Months { get; init; }

    public decimal Revenue(int month)
    {
        return Capital * Fee * month;
    }
}