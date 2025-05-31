namespace FinancialGoalsManager.Application.UseCases.Users.DeleteUser;

public sealed class DeleteUserUseCase(IFinancialGoalManagerDbContext context) : IDeleteUserUseCase
{
    public async Task<UseCaseResult<UseCaseResult>> ExecuteAsync(DeleteUserInputModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
        if (user is null)
            return new NotFoundResponse<UseCaseResult>(ErrorMessages.NotFound<User>());
        
        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return new NoContentResponse<UseCaseResult>();
    }
}