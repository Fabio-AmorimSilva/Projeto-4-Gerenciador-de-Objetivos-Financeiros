namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.GetFinancialGoal;

public interface IGetFinancialGoalUseCase
{
    Task<UseCaseResult<GetFinancialGoalUseCaseModel>> ExecuteAsync(Guid financialGoalId);
}