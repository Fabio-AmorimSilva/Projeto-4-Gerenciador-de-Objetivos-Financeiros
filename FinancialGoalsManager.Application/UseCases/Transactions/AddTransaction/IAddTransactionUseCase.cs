namespace FinancialGoalsManager.Application.UseCases.Transactions.AddTransaction;

public interface IAddTransactionUseCase
{
    Task<UseCaseResult<Guid>> ExecuteAsync(Guid financialGoalId, AddTransactionUseCaseInputModel model);
}