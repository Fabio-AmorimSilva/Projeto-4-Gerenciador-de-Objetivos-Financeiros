namespace FinancialGoalsManager.Application.UseCases.Users.AddUser;

public interface ICreateUserUseCase
{
    Task<UseCaseResult<Guid>> ExecuteAsync(CreateUserInputModel model);
}