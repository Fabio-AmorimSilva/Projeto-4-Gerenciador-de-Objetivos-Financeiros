namespace FinancialGoalsManager.Application.UseCases.Transactions.GetTransaction;

public interface IGetTransactionUseCase
{
    Task<UseCaseResult<GetTransactionUseCaseModel>> ExecuteAsync(Guid transactionId);
}