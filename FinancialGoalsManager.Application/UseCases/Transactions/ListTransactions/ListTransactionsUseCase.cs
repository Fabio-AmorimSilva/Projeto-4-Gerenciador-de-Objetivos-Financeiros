namespace FinancialGoalsManager.Application.UseCases.Transactions.ListTransactions;

public sealed class ListTransactionsUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : IListTransactionUseCase
{
    public async Task<UseCaseResult<IEnumerable<ListTransactionUseCaseModel>>> ExecuteAsync()
    {
        var transactions = await context.Users
            .AsNoTrackingWithIdentityResolution()
            .Where(u => u.Id == requestContextService.UserId)
            .SelectMany(u => u.Transactions)
            .Select(t => new ListTransactionUseCaseModel
            {
                Quantity = t.Quantity,
                Type = t.Type,
                Date = t.Date
            })
            .ToListAsync();

        return new OkResponse<IEnumerable<ListTransactionUseCaseModel>>(transactions);
    }
}