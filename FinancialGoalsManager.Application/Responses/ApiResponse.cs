namespace FinancialGoalsManager.Application.Responses;

public abstract class ApiResponse<T> : UseCaseResult<T>
{
    public int? StatusCode { get; set; }
}