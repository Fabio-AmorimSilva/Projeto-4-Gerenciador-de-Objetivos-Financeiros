namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.GetFinancialGoal;

public sealed class GetFinancialGoalUseCase(
    IFinancialGoalManagerDbContext context,
    IUserService userService
) : IGetFinancialGoalUseCase
{
    public async Task<UseCaseResult<GetFinancialGoalUseCaseModel>> ExecuteAsync(Guid financialGoalId)
    {
        var userId = userService.GetLoggedUserId();

        var financialGoal = await context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.FinancialGoals.Where(f => f.Id == financialGoalId))
            .Select(f => new GetFinancialGoalUseCaseModel
            {
                Title = f.Title,
                Goal = f.Goal,
                DueDate = f.DueDate,
                MonthGoal = f.MonthGoal,
                Status = f.Status,
                Total = f.Total
            })
            .FirstOrDefaultAsync();

        if (financialGoal is null)
            return new NotFoundResponse<GetFinancialGoalUseCaseModel>(ErrorMessages.NotFound<FinancialGoal>());
        
        return new OkResponse<GetFinancialGoalUseCaseModel>(financialGoal);
    }
}