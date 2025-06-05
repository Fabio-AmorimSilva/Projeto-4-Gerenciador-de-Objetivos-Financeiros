namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.ListFinancialGoals;

public sealed class ListFinancialGoalsUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : IListFinancialGoalsUseCase
{
    public async Task<UseCaseResult<IEnumerable<ListFinancialGoalsUseCaseModel>>> ExecuteAsync()
    {
        var financialGoals = await context.Users
            .SelectMany(u => u.FinancialGoals.Where(f => f.UserId == requestContextService.UserId))
            .Select(f => new ListFinancialGoalsUseCaseModel
            {
                Title = f.Title,
                Goal = f.Goal,
                DueDate = f.DueDate,
                MonthGoal = f.MonthGoal,
                Status = f.Status,
                Total = f.Total
            })
            .ToListAsync();

        return new OkResponse<IEnumerable<ListFinancialGoalsUseCaseModel>>(financialGoals);
    }
}