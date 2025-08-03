namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.TrackFinancialGoalProgress;

public sealed class TrackFinancialGoalProgressUseCase(IFinancialGoalManagerDbContext context) : ITrackFinancialGoalProgress
{
    public async Task<UseCaseResult<IEnumerable<TrackFinancialGoalProgressUseCaseOuputModel>>> ExecuteAsync()
    {
        var transactionByMonthAndYear = context.Users
            .AsNoTracking()
            .SelectMany(u => u.Transactions)
            .GroupBy(t => new
            {
                t.Date.Month,
                t.Date.Year,
                t.FinancialGoal.Title,
                t.FinancialGoal.MonthGoal
            })
            .Select(g => new
            {
                g.Key.Month,
                g.Key.Year,
                g.Key.Title,
                g.Key.MonthGoal
            });

        var transactionByMonthAndYearAndQuantity = context.Users
            .AsNoTracking()
            .SelectMany(u => u.Transactions)
            .GroupBy(t => new
            {
                t.Date.Month,
                t.Date.Year,
                t.Quantity
            })
            .Select(g => new
            {
                g.Key.Month,
                g.Key.Year,
                g.Key.Quantity
            });

        var totalByMonthAndYear = transactionByMonthAndYear
            .AsNoTracking()
            .Select(tym => new
            {
                tym.Month,
                tym.Year,
                Total = transactionByMonthAndYearAndQuantity
                    .Where(tymq =>
                        tymq.Month == tym.Month &&
                        tymq.Year == tym.Year
                    )
                    .Sum(tymq => tymq.Quantity)
            });

        var trackingByMonthAndYear = await transactionByMonthAndYear
            .AsNoTracking()
            .Select(tym => new TrackFinancialGoalProgressUseCaseOuputModel
            {
                Month = tym.Month,
                Year = tym.Year,
                FinancialGoalName = tym.Title,
                Total = totalByMonthAndYear
                    .First(t =>
                        t.Month == tym.Month &&
                        t.Year == tym.Year).Total,
                Status = totalByMonthAndYear
                    .First(t =>
                        t.Month == tym.Month &&
                        t.Year == tym.Year).Total >= tym.MonthGoal
                    ? GoalStatus.Success
                    : GoalStatus.Failed
            }).ToListAsync();

        return new OkResponse<IEnumerable<TrackFinancialGoalProgressUseCaseOuputModel>>(trackingByMonthAndYear);
    }
}