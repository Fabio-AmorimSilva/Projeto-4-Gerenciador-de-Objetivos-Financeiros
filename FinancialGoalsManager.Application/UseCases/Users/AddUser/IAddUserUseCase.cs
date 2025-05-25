namespace FinancialGoalsManager.Application.UseCases.Users.AddUser;

public interface IAddUserUseCase
{
    Task<UseCaseResult<Guid>> Execute(AddUserInputModel model);
}