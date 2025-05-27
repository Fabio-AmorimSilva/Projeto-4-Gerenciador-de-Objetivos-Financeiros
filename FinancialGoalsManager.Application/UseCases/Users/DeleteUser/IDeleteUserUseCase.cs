namespace FinancialGoalsManager.Application.UseCases.Users.DeleteUser;

public interface IDeleteUserUseCase
{
    Task<UseCaseResult<UseCaseResult>> Execute(DeleteUserInputModel model);
}