namespace FinancialGoalsManager.Infrastructure.Reports.FinancialGoal;

public sealed class FinancialGoalReportService(ITrackFinancialGoalProgress useCase) : IFinancialGoalReportService
{
    public async Task<byte[]> GeneratePdf()
    {
        var trackings = await useCase.ExecuteAsync();

        if (trackings.Data is null)
            return [];

        var models = trackings.Data.Select(
            d => new FinancialGoalReportModel
            {
                Month = d.Month,
                Year = d.Year,
                Total = d.Total
            });

        var pdf = FinancialGoalReport.GeneratePdf(models);

        return pdf;
    }
}