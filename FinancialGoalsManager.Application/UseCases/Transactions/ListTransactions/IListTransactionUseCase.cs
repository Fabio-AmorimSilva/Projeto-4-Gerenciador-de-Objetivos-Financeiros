namespace FinancialGoalsManager.Application.UseCases.Transactions.ListTransactions;

public interface IListTransactionUseCase
{
    Task<UseCaseResult<IEnumerable<ListTransactionUseCaseModel>>> ExecuteAsync();
}