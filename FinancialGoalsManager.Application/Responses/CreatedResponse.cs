namespace FinancialGoalsManager.Application.Responses;

public class CreatedResponse<T> : ApiResponse<T>
{
    public CreatedResponse(T data)
    {
        Data = data;
        StatusCode = 201;
    }
}