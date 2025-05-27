namespace FinancialGoalsManager.Application.Responses;

public class NotFoundResponse<T> : ApiResponse<T>
{
    public NotFoundResponse(string message)
    {
        Error(message);
        StatusCode = 404;
    }
}