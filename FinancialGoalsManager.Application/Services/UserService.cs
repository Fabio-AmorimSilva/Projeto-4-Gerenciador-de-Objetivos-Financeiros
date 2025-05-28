namespace FinancialGoalsManager.Application.Services;

public sealed class UserService(IHttpContextAccessor acessor)
{
    public string GetLoggedUserId()
    {
        var userId = acessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

        return userId ?? string.Empty;
    }
}