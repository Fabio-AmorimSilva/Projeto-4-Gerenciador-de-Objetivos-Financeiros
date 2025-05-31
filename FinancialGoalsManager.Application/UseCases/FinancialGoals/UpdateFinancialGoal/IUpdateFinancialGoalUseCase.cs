namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.UpdateFinancialGoal;

public interface IUpdateFinancialGoalUseCase
{
    Task<UseCaseResult<UseCaseResult>> ExecuteAsync(Guid financialGoalId, UpdateFinancialGoalUseCaseInputModel model);
}