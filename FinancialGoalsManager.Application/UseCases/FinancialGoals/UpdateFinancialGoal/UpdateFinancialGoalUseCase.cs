namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.UpdateFinancialGoal;

public sealed class UpdateFinancialGoalUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : IUpdateFinancialGoalUseCase
{
    public async Task<UseCaseResult<UseCaseResult>> ExecuteAsync(Guid financialGoalId, UpdateFinancialGoalUseCaseInputModel model)
    {
        var user = await context.Users
            .Include(f => f.FinancialGoals.Where(f => f.Id == financialGoalId))
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            return new NotFoundResponse<UseCaseResult>(ErrorMessages.NotFound<User>());

        var result = user.UpdateGoal(
            financialGoalId: financialGoalId,
            dueDate: model.DueDate,
            monthGoal: model.MonthGoal,
            title: model.Title,
            goal: model.Goal,
            status: model.Status
        );
        
        if(!result.IsSuccess)
            return new NotFoundResponse<UseCaseResult>(result.Message);
        
        await context.SaveChangesAsync();

        return new NoContentResponse<UseCaseResult>();
    }
}