namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.AddFinancialGoal;

public sealed class AddFinancialGoalUseCase(
    IFinancialGoalManagerDbContext context,
    UserService userService
) : IAddFinancialGoalUseCase
{
    public async Task<UseCaseResult<Guid>> ExecuteAsync(AddFinancialGoalInputModel model)
    {
        var userId = userService.GetLoggedUserId();
        
        var user = await context.Users
            .Include(u => u.FinancialGoals)
            .FirstOrDefaultAsync(u => u.Id == userId);
        
        if (user is null)
            return new NotFoundResponse<Guid>(ErrorMessages.NotFound<User>());

        var financialGoal = new FinancialGoal(
            title: model.Title,
            goal: model.Goal,
            dueDate: model.DueDate,
            monthGoal: model.MonthGoal,
            status: model.Status,
            user: user
        );

        user.AddGoal(financialGoal);
        await context.SaveChangesAsync();

        return new CreatedResponse<Guid>(financialGoal.Id);
    }
}