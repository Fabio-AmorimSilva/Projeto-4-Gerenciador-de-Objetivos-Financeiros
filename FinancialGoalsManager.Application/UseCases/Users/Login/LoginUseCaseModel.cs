namespace FinancialGoalsManager.Application.UseCases.Users.Login;

public sealed record LoginUseCaseModel
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}