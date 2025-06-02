namespace FinancialGoalsManager.Application.UseCases.Transactions.AddTransaction;

public sealed class AddTransactionUseCase(
    IFinancialGoalManagerDbContext context,
    IUserService userService
) : IAddTransactionUseCase
{
    public async Task<UseCaseResult<Guid>> ExecuteAsync(Guid financialGoalId, AddTransactionUseCaseInputModel model)
    {
        var userId = userService.GetLoggedUserId();
        var user = await context.Users
            .Include(u => u.Transactions)
            .Include(u => u.FinancialGoals.Where(f => f.Id == financialGoalId))
            .FirstOrDefaultAsync(u => u.Id == userId);

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