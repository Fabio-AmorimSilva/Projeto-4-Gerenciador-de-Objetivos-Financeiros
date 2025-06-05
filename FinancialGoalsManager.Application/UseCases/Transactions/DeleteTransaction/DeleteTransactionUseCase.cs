namespace FinancialGoalsManager.Application.UseCases.Transactions.DeleteTransaction;

public sealed class DeleteTransactionUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : IDeleteTransactionUseCase
{
    public async Task<UseCaseResult> ExecuteAsync(Guid transactionId)
    {
        var user = await context.Users
            .Include(u => u.Transactions.Where(t => t.Id == transactionId))
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            return new NotFoundResponse<User>(ErrorMessages.NotFound<User>());

        var transaction = user.GetTransaction(transactionId);
        if (transaction is null)
            return new NotFoundResponse<Transaction>(ErrorMessages.NotFound<Transaction>());

        user.DeleteTransaction(transaction);
        await context.SaveChangesAsync();

        return new NoContentResponse<UseCaseResult>();
    }
}