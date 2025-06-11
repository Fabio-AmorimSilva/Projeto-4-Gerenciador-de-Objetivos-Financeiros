namespace FinancialGoalsManager.Api.Endpoints;

public static class TransactionEndpoints
{
    public static void Map(WebApplication app)
    {
        const string url = "/api/transactions";

        var mapGroup = app.MapGroup(url)
            .RequireAuthorization();

        mapGroup.MapGet("/",
            [ProducesResponseType(typeof(UseCaseResult<UseCaseResult<ListTransactionUseCaseModel>>), StatusCodes.Status200OK)]
            async ([FromServices] IListTransactionUseCase useCase) =>
            {
                var response = await useCase.ExecuteAsync();

                return Results.Ok(response);
            });
        
        mapGroup.MapGet("/{transactionId}",
            [ProducesResponseType(typeof(UseCaseResult<UseCaseResult<GetTransactionUseCaseModel>>), StatusCodes.Status200OK)]
            async ([FromRoute] Guid transactionId, [FromServices] IGetTransactionUseCase useCase) =>
            {
                var response = await useCase.ExecuteAsync(transactionId);

                return Results.Ok(response);
            });

        mapGroup.MapPost("{financialGoalId:guid}",
            [ProducesResponseType(typeof(UseCaseResult<Guid>), StatusCodes.Status201Created)]
            async ([FromRoute] Guid financialGoalId, [FromServices] CreateTransactionUseCase useCase, [FromBody] CreateTransactionUseCaseInputModel model) =>
            {
                var response = await useCase.ExecuteAsync(financialGoalId, model);

                return Results.Created(url, response);
            });

        mapGroup.MapDelete("{transactionId:guid}/delete",
            [ProducesResponseType(typeof(UseCaseResult), StatusCodes.Status204NoContent)]
            async ([FromRoute] Guid transactionId, [FromServices] IDeleteTransactionUseCase useCase) =>
            {
                await useCase.ExecuteAsync(transactionId);

                return Results.NoContent();
            });
    }
}