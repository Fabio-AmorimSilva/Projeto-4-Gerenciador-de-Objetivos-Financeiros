namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.DeleteFinancialGoal;

public sealed class DeleteFinancialGoalUseCase(
    IFinancialGoalManagerDbContext context,
    IUserService userService
) : IDeleteFinancialGoalUseCase
{
    public async Task<UseCaseResult<UseCaseResult>> Execute(DeleteFinancialGoalUseCaseInputModel model)
    {
        var userId = userService.GetLoggedUserId();
        
        var user = await context.Users
            .Include(u => u.FinancialGoals.Where(f => f.Id == model.FinancialGoalId))
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return new NotFoundResponse<UseCaseResult>(ErrorMessages.NotFound<User>());
        
        var financialGoal = user.GetGoal(model.FinancialGoalId);
        
        if (financialGoal is null)
            return new NotFoundResponse<UseCaseResult>(ErrorMessages.NotFound<FinancialGoal>());
        
        user.DeleteGoal(financialGoal);
        await context.SaveChangesAsync();

        return new NoContentResponse<UseCaseResult>();
    }
}