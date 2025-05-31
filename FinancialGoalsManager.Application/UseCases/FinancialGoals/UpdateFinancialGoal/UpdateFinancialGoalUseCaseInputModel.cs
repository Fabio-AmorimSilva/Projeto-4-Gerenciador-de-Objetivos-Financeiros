namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.UpdateFinancialGoal;

public sealed record UpdateFinancialGoalUseCaseInputModel
{
    public string Title { get; set; } = null!;
    public decimal Goal { get; set; }
    public DateTime DueDate { get; set; }
    public decimal? MonthGoal { get; set; }
    public GoalStatus Status { get; set; }
}