namespace FinancialGoalsManager.Application.UseCases.Users.UpdateUser;

public sealed record UpdateUserInputModel
{
    public required Guid UserId { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}