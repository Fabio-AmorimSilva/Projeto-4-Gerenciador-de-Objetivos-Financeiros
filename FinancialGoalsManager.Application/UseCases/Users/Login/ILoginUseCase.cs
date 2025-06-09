namespace FinancialGoalsManager.Application.UseCases.Users.Login;

public interface ILoginUseCase
{
    Task<UseCaseResult<string>> ExecuteAsync(LoginUseCaseModel model);
}