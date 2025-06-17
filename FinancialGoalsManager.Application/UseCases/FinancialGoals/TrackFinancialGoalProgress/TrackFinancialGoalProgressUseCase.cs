namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.TrackFinancialGoalProgress;

public sealed class TrackFinancialGoalProgressUseCase(IFinancialGoalManagerDbContext context) : ITrackFinancialGoalProgress
{
    public async Task<UseCaseResult<IEnumerable<TrackFinancialGoalProgressUseCaseOuputModel>>> ExecuteAsync()
    {
        var transactionByMonthAndYear = context.Users
            .SelectMany(u => u.Transactions)
            .GroupBy(t => new
            {
                t.Date.Month,
                t.Date.Year
            })
            .Select(g => new
            {
                g.Key.Month,
                g.Key.Year
            });

        var transactionByMonthAndYearAndQuantity = context.Users
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

        var trackingByMonthAndYear = await transactionByMonthAndYear
            .Select(tym => new TrackFinancialGoalProgressUseCaseOuputModel
            {
                Month = tym.Month,
                Year = tym.Year,
                Total = transactionByMonthAndYearAndQuantity
                    .Where(tymq =>
                        tymq.Month == tym.Month &&
                        tymq.Year == tym.Year
                    )
                    .Sum(tymq => tymq.Quantity)
            }).ToListAsync();

        return new OkResponse<IEnumerable<TrackFinancialGoalProgressUseCaseOuputModel>>(trackingByMonthAndYear);
    }
}