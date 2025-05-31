namespace FinancialGoalsManager.Application.UseCases.Users.UpdatePassword;

public sealed class UpdatePasswordUseCase(IFinancialGoalManagerDbContext context) : IUpdatePasswordUseCase
{
    public async Task<UseCaseResult<UseCaseResult>> ExecuteAsync(UpdatePasswordInputModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
        if (user is null)
            return new NotFoundResponse<UseCaseResult>(ErrorMessages.NotFound<User>());

        user.UpdatePassword(model.NewPassword);

        await context.SaveChangesAsync();

        return new NoContentResponse<UseCaseResult>();
    }
}