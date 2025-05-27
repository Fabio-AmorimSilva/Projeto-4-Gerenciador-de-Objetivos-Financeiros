namespace FinancialGoalsManager.Application.Common.Responses;

public class NotFoundResponse<T> : ApiResponse<T>
{
    public NotFoundResponse(string message)
    {
        Error(message);
        StatusCode = 404;
    }
}