namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.DeleteFinancialGoal;

public interface IDeleteFinancialGoalUseCase
{
    Task<UseCaseResult<UseCaseResult>> Execute(DeleteFinancialGoalUseCaseInputModel inputModel);
}