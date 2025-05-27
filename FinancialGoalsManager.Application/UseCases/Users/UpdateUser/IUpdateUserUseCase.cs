namespace FinancialGoalsManager.Application.UseCases.Users.UpdateUser;

public interface IUpdateUserUseCase
{
    Task<UseCaseResult<UseCaseResult>> Execute(UpdateUserInputModel model);
}