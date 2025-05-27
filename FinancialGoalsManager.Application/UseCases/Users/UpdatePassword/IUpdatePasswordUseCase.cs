namespace FinancialGoalsManager.Application.UseCases.Users.UpdatePassword;

public interface IUpdatePasswordUseCase
{
    Task<UseCaseResult<UseCaseResult>> Execute(UpdatePasswordInputModel model);
}