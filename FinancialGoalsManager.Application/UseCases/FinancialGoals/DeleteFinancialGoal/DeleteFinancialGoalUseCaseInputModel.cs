namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.DeleteFinancialGoal;

public sealed record DeleteFinancialGoalUseCaseInputModel
{
    public Guid FinancialGoalId { get; set; }
}