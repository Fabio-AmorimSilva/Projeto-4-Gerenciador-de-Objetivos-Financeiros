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
            async (Guid transactionId, IListTransactionUseCase useCase) =>
            {
                var response = await useCase.ExecuteAsync();

                return Results.Ok(response);
            });
        
        mapGroup.MapGet("/{transactionId}",
            [ProducesResponseType(typeof(UseCaseResult<UseCaseResult<GetTransactionUseCaseModel>>), StatusCodes.Status200OK)]
            async (Guid transactionId, IGetTransactionUseCase useCase) =>
            {
                var response = await useCase.ExecuteAsync(transactionId);

                return Results.Ok(response);
            });

        mapGroup.MapPost("{financialGoalId:guid}",
            [ProducesResponseType(typeof(UseCaseResult<Guid>), StatusCodes.Status201Created)]
            async (Guid financialGoalId, AddTransactionUseCase useCase, AddTransactionUseCaseInputModel model) =>
            {
                var response = await useCase.ExecuteAsync(financialGoalId, model);

                return Results.Created(url, response);
            });

        mapGroup.MapDelete("{transactionId:guid}/delete",
            [ProducesResponseType(typeof(UseCaseResult), StatusCodes.Status204NoContent)]
            async (Guid financialGoalId, Guid transactionId, IDeleteTransactionUseCase useCase) =>
            {
                await useCase.ExecuteAsync(transactionId);

                return Results.NoContent();
            });
    }
}