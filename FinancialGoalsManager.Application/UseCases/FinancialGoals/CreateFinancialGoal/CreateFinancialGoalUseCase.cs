﻿namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.CreateFinancialGoal;

public sealed class CreateFinancialGoalUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : ICreateFinancialGoalUseCase
{
    public async Task<UseCaseResult<Guid>> ExecuteAsync(CreateFinancialGoalInputModel model)
    {
        var user = await context.Users
            .Include(u => u.FinancialGoals)
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);
        
        if (user is null)
            return new NotFoundResponse<Guid>(ErrorMessages.NotFound<User>());

        var financialGoal = new FinancialGoal(
            title: model.Title,
            goal: model.Goal,
            dueDate: model.DueDate,
            monthGoal: model.MonthGoal,
            user: user
        );

        user.AddGoal(financialGoal);
        await context.SaveChangesAsync();

        return new CreatedResponse<Guid>(financialGoal.Id);
    }
}