namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.TrackFinancialGoalsReport;

public interface ITrackFinancialGoalsReportUseCase
{
    Task<UseCaseResult<TrackFinancialGoalsReportUseCaseOutputModel>> ExecuteAsync();

}