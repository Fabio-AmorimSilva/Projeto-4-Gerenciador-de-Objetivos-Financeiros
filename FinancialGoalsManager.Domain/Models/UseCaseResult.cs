namespace FinancialGoalsManager.Domain.Models;

public class UseCaseResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    protected UseCaseResult()
    {
    }

    public static UseCaseResult Success()
        => new()
        {
            IsSuccess = true
        };

    public static UseCaseResult Error(string message)
        => new UseCaseResult()
        {
            IsSuccess = false,
            Message = message
        };
}

public class UseCaseResult<T> : UseCaseResult
{
    public T? Data { get; set; }

    public static UseCaseResult<T> Success(T? data)
        => new()
        {
            IsSuccess = true,
            Data = data
        };

    public new static UseCaseResult<T> Error(string message)
        => new()
        {
            IsSuccess = false,
            Message = message
        };
}