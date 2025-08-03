namespace FinancialGoalsManager.Application.UseCases.Transactions.GetTransaction;

public sealed class GetTransactionUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : IGetTransactionUseCase
{
    public async Task<UseCaseResult<GetTransactionUseCaseModel>> ExecuteAsync(Guid transactionId)
    {
        var transaction = await context.Users
            .AsNoTrackingWithIdentityResolution()
            .Where(u => u.Id == requestContextService.UserId)
            .SelectMany(u => u.Transactions.Where(t => t.Id == transactionId))
            .Select(t => new GetTransactionUseCaseModel
            {
                Quantity = t.Quantity,
                Type = t.Type,
                Date = t.Date
            })
            .FirstOrDefaultAsync();

        if (transaction is null)
            return new NotFoundResponse<GetTransactionUseCaseModel>(ErrorMessages.NotFound<Transaction>());

        return new OkResponse<GetTransactionUseCaseModel>(transaction);
    }
}