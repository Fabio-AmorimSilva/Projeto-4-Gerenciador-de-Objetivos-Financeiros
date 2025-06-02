namespace FinancialGoalsManager.Application.UseCases.Transactions.DeleteTransaction;

public interface IDeleteTransactionUseCase
{
    Task<UseCaseResult> ExecuteAsync(Guid transactionId);
}