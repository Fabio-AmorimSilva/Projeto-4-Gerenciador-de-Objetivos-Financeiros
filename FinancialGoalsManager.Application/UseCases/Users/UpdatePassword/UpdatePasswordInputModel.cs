namespace FinancialGoalsManager.Application.UseCases.Users.UpdatePassword;

public sealed record UpdatePasswordInputModel
{
    public required Guid UserId { get; init; }
    public required string NewPassword { get; init; }
}