namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.TrackFinancialGoalProgress;

public interface ITrackFinancialGoalProgress
{
    Task<UseCaseResult<IEnumerable<TrackFinancialGoalProgressUseCaseOuputModel>>> ExecuteAsync();
}