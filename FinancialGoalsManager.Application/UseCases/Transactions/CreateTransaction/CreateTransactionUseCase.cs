namespace FinancialGoalsManager.Application.UseCases.Transactions.CreateTransaction;

public sealed class CreateTransactionUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : ICreateTransactionUseCase
{
    public async Task<UseCaseResult<Guid>> ExecuteAsync(Guid financialGoalId, CreateTransactionUseCaseInputModel model)
    {
        var user = await context.Users
            .Include(u => u.Transactions.Where(t => t.FinancialGoalId == financialGoalId))
            .Include(u => u.FinancialGoals.Where(f => f.Id == financialGoalId))
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            return new NotFoundResponse<Guid>(ErrorMessages.NotFound<User>());

        var financialGoal = user.GetGoal(financialGoalId);
        if (financialGoal is null)
            return new NotFoundResponse<Guid>(ErrorMessages.NotFound<FinancialGoal>());

        var transaction = new Transaction(
            quantity: model.Quantity,
            type: model.Type,
            date: model.Date,
            financialGoal: financialGoal,
            user: user
        );

        user.AddTransaction(transaction);
        await context.SaveChangesAsync();

        return new CreatedResponse<Guid>(transaction.Id);
    }
}