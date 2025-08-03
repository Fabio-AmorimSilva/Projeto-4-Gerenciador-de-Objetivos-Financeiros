namespace FinancialGoalsManager.Application.UseCases.Users.UpdateUser;

public sealed class UpdateUserUseCase(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService
) : IUpdateUserUseCase
{
    public async Task<UseCaseResult<UseCaseResult>> ExecuteAsync(UpdateUserInputModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);
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