namespace FinancialGoalsManager.Application.UseCases.Users.DeleteUser;

public interface IDeleteUserUseCase
{
    Task<UseCaseResult<UseCaseResult>> ExecuteAsync(DeleteUserInputModel model);
}