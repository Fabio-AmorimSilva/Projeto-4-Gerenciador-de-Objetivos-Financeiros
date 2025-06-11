namespace FinancialGoalsManager.Application.Common.Responses;

public class NoContentResponse<T> : ApiResponse<T>
{
    public NoContentResponse()
    {
        StatusCode = 204;
        IsSuccess = false;
    }
}