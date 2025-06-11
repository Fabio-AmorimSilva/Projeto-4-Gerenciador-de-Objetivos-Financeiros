namespace FinancialGoalsManager.Application.UseCases.Users.CreateUser;

public interface ICreateUserUseCase
{
    Task<UseCaseResult<Guid>> ExecuteAsync(CreateUserInputModel model);
}