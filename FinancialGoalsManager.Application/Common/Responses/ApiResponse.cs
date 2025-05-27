namespace FinancialGoalsManager.Application.Common.Responses;

public abstract class ApiResponse<T> : UseCaseResult<T>
{
    public int? StatusCode { get; set; }
}