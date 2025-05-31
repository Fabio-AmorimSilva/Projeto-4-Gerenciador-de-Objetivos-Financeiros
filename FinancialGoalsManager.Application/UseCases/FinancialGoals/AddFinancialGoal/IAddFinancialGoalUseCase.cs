namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.AddFinancialGoal;

public interface IAddFinancialGoalUseCase
{
    Task<UseCaseResult<Guid>> ExecuteAsync(AddFinancialGoalInputModel model);
}