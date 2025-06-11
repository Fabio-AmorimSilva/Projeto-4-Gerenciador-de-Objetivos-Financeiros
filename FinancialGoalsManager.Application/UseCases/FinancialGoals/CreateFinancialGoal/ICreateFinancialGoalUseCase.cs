namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.CreateFinancialGoal;

public interface ICreateFinancialGoalUseCase
{
    Task<UseCaseResult<Guid>> ExecuteAsync(CreateFinancialGoalInputModel model);
}