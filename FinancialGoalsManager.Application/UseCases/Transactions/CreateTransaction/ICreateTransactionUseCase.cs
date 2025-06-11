namespace FinancialGoalsManager.Application.UseCases.Transactions.CreateTransaction;

public interface ICreateTransactionUseCase
{
    Task<UseCaseResult<Guid>> ExecuteAsync(Guid financialGoalId, CreateTransactionUseCaseInputModel model);
}