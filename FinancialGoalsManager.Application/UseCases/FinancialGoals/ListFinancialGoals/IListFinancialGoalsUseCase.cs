namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.ListFinancialGoals;

public interface IListFinancialGoalsUseCase
{
    Task<UseCaseResult<IEnumerable<ListFinancialGoalsUseCaseModel>>> ExecuteAsync();
}