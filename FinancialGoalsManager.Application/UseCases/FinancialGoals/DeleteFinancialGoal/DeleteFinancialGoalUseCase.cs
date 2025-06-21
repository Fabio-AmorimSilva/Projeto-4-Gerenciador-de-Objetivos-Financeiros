namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.DeleteFinancialGoal;

public sealed class DeleteFinancialGoalUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : IDeleteFinancialGoalUseCase
{
    public async Task<UseCaseResult<UseCaseResult>> ExecuteAsync(Guid financialGoalId)
    {
        var user = await context.Users
            .Include(u => u.FinancialGoals.Where(f => f.Id == financialGoalId))
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            return new NotFoundResponse<UseCaseResult>(ErrorMessages.NotFound<User>());
        
        var financialGoal = user.GetGoal(financialGoalId);
        
        if (financialGoal is null)
            return new NotFoundResponse<UseCaseResult>(ErrorMessages.NotFound<FinancialGoal>());
        
        user.DeleteGoal(financialGoal);
        await context.SaveChangesAsync();

        return new NoContentResponse<UseCaseResult>();
    }
}