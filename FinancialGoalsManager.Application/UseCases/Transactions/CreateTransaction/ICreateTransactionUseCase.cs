namespace FinancialGoalsManager.Application.UseCases.Transactions.AddTransaction;

public interface ICreateTransactionUseCase
{
    Task<UseCaseResult<Guid>> ExecuteAsync(Guid financialGoalId, CreateTransactionUseCaseInputModel model);
}