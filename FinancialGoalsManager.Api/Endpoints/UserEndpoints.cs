namespace FinancialGoalsManager.Api.Endpoints;

public static class UserEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("/api/users")
            .RequireAuthorization();
        
        const string url = "/api/users";
        
        mapGroup.MapPost(
             "/", 
             [ProducesResponseType(typeof(UseCaseResult<Guid>), StatusCodes.Status201Created)]
             [AllowAnonymous] async ([FromBody] AddUserInputModel model, [FromServices] IAddUserUseCase userCase)  =>
        {
            var userId = await userCase.ExecuteAsync(model);

            return Results.Created(url, userId);
        }).AddEndpointFilter<ModelStateValidatorFilter<AddUserInputModel>>();
        
        mapGroup.MapPut("/update", async ([FromBody] UpdateUserInputModel model, [FromServices] IUpdateUserUseCase userCase)  =>
        {
            await userCase.ExecuteAsync(model);

            return Results.NoContent();
        }).AddEndpointFilter<ModelStateValidatorFilter<UpdateUserInputModel>>();

        mapGroup.MapDelete("/delete", async ([FromBody] DeleteUserInputModel model, [FromServices] IDeleteUserUseCase userCase) =>
        {
            await userCase.ExecuteAsync(model);

            return Results.NoContent();
        }).AddEndpointFilter<ModelStateValidatorFilter<DeleteUserInputModel>>();
    }
}