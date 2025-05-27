namespace FinancialGoalsManager.Application.UseCases.Users.DeleteUser;

public sealed record DeleteUserInputModel
{
    public Guid UserId { get; init; }
}