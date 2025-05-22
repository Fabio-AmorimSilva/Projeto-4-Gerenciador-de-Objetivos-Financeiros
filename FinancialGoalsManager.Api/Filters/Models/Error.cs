namespace FinancialGoalsManager.Api.Filters.Models;

public sealed class Error
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
}