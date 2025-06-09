namespace FinancialGoalsManager.Application.UseCases.Users.Login;

public sealed class LoginUseCase(
    IFinancialGoalManagerDbContext context,
    IPasswordHashService passwordHashService,
    ITokenService tokenService
) : ILoginUseCase
{
    public async Task<UseCaseResult<string>> ExecuteAsync(LoginUseCaseModel model)
    {
        var passwordHash = passwordHashService.HashPassword(model.Password);

        var user = await context.Users
            .FirstOrDefaultAsync(u =>
                u.Email == model.Email &&
                u.Password == passwordHash
            );

        if (user is null)
            return new UnprocessableResponse<string>(ErrorMessages.EmailOrPasswordAreIncorrect());

        var token = tokenService.GenerateToken(user);

        return new OkResponse<string>(token);
    }
}