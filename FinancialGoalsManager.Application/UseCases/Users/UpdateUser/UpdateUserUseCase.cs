using FinancialGoalsManager.Application.Common.Responses;

namespace FinancialGoalsManager.Application.UseCases.Users.UpdateUser;

public sealed class UpdateUserUseCase(IFinancialGoalManagerDbContext context) : IUpdateUserUseCase
{
    public async Task<UseCaseResult<UseCaseResult>> Execute(UpdateUserInputModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
        if (user is null)
            return new NotFoundResponse<UseCaseResult>(ErrorMessages.NotFound<User>());

        user.Update(
            name: model.Name,
            email: model.Email
        );

        await context.SaveChangesAsync();

        return new NoContentResponse<UseCaseResult>();
    }
}