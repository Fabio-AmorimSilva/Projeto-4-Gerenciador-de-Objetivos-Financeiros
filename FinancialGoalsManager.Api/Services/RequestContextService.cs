namespace FinancialGoalsManager.Api.Services;

public sealed class RequestContextService(IHttpContextAccessor httpContextAccessor) : IRequestContextService
{
    public Guid UserId => new(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid) ?? string.Empty);
}