namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.SimulateFinancialGoalProgress;

public class SimulateFinancialGoalProgressUseCase : ISimulateFinancialGoalProgressUseCase
{
    public UseCaseResult<IEnumerable<SimulateFinancialGoalProgressUseCaseOuputModel>> Execute(SimulateFinancialGoalProgressUseCaseInputModel model)
    {
        var models = new List<SimulateFinancialGoalProgressUseCaseOuputModel>();

        for (var i = 0; i < model.Months; i++)
        {
            var date = DateTime.Today.AddMonths(i);

            models.Add(new SimulateFinancialGoalProgressUseCaseOuputModel
            {
                Month = date.Month,
                Year = date.Year,
                Total = model.Revenue(i)
            });
        }

        var orderedModels = models
            .OrderBy(m => m.Year)
            .ThenBy(m => m.Month);

        return new OkResponse<IEnumerable<SimulateFinancialGoalProgressUseCaseOuputModel>>(orderedModels);
    }
}