namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.AddFinancialGoal;

public interface ICreateFinancialGoalUseCase
{
    Task<UseCaseResult<Guid>> ExecuteAsync(CreateFinancialGoalInputModel model);
}