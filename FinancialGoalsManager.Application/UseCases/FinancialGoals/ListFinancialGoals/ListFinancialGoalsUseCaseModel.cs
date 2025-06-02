namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.ListFinancialGoals;

public sealed record ListFinancialGoalsUseCaseModel
{
    public string Title { get; set; } = null!;
    public decimal Goal { get; set; }
    public DateTime DueDate { get; set; }
    public decimal? MonthGoal { get; set; }
    public GoalStatus Status { get; set; }
    public decimal Total { get; set; }
}