namespace FinancialGoalsManager.Api.Endpoints;

public static class FinancialGoalEndpoints
{
    public static void Map(WebApplication app)
    {
        const string url = "/api/financial-goals";

        var mapGroup = app.MapGroup(url)
            .RequireAuthorization();

        mapGroup.MapGet("/financial-progress",
            [ProducesResponseType(typeof(UseCaseResult<SimulateFinancialGoalProgressUseCaseOuputModel>), StatusCodes.Status200OK)]
            ([FromServices] ISimulateFinancialGoalProgressUseCase useCase, [FromBody] SimulateFinancialGoalProgressUseCaseInputModel model) =>
            {
                var response = useCase.Execute(model);

                return Results.Ok(response);
            });

        mapGroup.MapGet("/financial-tracking",
            [ProducesResponseType(typeof(TrackFinancialGoalProgressUseCaseOuputModel), StatusCodes.Status200OK)]
            async ([FromServices] ITrackFinancialGoalProgress useCase) =>
            {
                var response = await useCase.ExecuteAsync();

                return Results.Ok(response);
            });

        mapGroup.MapGet("/{financialGoalId:guid}",
            [ProducesResponseType(typeof(UseCaseResult<GetFinancialGoalUseCaseModel>), StatusCodes.Status200OK)]
            async ([FromRoute] Guid financialGoalId, [FromServices] IGetFinancialGoalUseCase useCase) =>
            {
                var response = await useCase.ExecuteAsync(financialGoalId);

                return Results.Ok(response);
            });

        mapGroup.MapGet("/",
            [ProducesResponseType(typeof(UseCaseResult<GetFinancialGoalUseCaseModel>), StatusCodes.Status200OK)]
            async ([FromServices] IListFinancialGoalsUseCase useCase) =>
            {
                var response = await useCase.ExecuteAsync();

                return Results.Ok(response);
            });

        mapGroup.MapPost("/",
            [ProducesResponseType(typeof(UseCaseResult<Guid>), StatusCodes.Status201Created)]
            async ([FromServices] ICreateFinancialGoalUseCase useCase, [FromBody] CreateFinancialGoalInputModel model) =>
            {
                var response = await useCase.ExecuteAsync(model);

                return Results.Created(url, response);
            }).AddEndpointFilter<ModelStateValidatorFilter<CreateFinancialGoalInputModel>>();

        mapGroup.MapPut("/{financialGoalId:guid}/update",
            [ProducesResponseType(typeof(UseCaseResult), StatusCodes.Status204NoContent)]
            async ([FromRoute] Guid financialGoalId, [FromServices] IUpdateFinancialGoalUseCase useCase, [FromBody] UpdateFinancialGoalUseCaseInputModel model) =>
            {
                await useCase.ExecuteAsync(financialGoalId, model);

                return Results.NoContent();
            }).AddEndpointFilter<ModelStateValidatorFilter<UpdateFinancialGoalUseCaseInputModel>>();

        mapGroup.MapDelete("/{financialGoalId:guid}/delete",
            [ProducesResponseType(typeof(UseCaseResult), StatusCodes.Status204NoContent)]
            async ([FromRoute] Guid financialGoalId, [FromServices] IDeleteFinancialGoalUseCase useCase, [FromBody] DeleteFinancialGoalUseCaseInputModel model) =>
            {
                model.FinancialGoalId = financialGoalId;

                await useCase.ExecuteAsync(model);

                return Results.NoContent();
            }).AddEndpointFilter<ModelStateValidatorFilter<DeleteFinancialGoalUseCaseInputModel>>();
    }
}