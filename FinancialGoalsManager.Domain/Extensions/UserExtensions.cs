namespace FinancialGoalsManager.Domain.Entities;

public static class UserExtensions
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        return new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };
    }
}