namespace FinancialGoalsManager.Application.Services;

public sealed class UserService(IHttpContextAccessor acessor) : IUserService
{
    public Guid GetLoggedUserId()
    {
        var userId = acessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

        return new Guid(userId ?? string.Empty);
    }
}