namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.UpdateFinancialGoal;

public interface IUpdateFinancialGoalUseCase
{
    Task<UseCaseResult<UseCaseResult>> Execute(Guid financialGoalId, UpdateFinancialGoalUseCaseInputModel model);
}