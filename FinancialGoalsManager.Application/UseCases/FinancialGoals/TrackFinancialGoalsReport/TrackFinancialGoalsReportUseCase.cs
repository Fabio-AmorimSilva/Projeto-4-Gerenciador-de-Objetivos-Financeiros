namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.TrackFinancialGoalsReport;

public sealed class TrackFinancialGoalsReportUseCase(
    IFinancialGoalReportService financialGoalReportService
) : ITrackFinancialGoalsReportUseCase
{
    public async Task<UseCaseResult<TrackFinancialGoalsReportUseCaseOutputModel>> ExecuteAsync()
    {
       var pdf = await financialGoalReportService.GeneratePdf();

       return new OkResponse<TrackFinancialGoalsReportUseCaseOutputModel>(new TrackFinancialGoalsReportUseCaseOutputModel
       {
           Data = pdf
       });
    }
}