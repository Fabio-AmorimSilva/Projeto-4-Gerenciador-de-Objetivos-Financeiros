namespace FinancialGoalsManager.Api.Endpoints;

public static class UserEndpoints
{
    public static void Map(WebApplication app)
    {
        const string url = "/api/users";
        
        var mapGroup = app.MapGroup(url)
            .RequireAuthorization();

        mapGroup.MapGet("/login",
            [ProducesResponseType(typeof(UseCaseResult<string>), StatusCodes.Status200OK)]
            [AllowAnonymous] async ([FromServices] ILoginUseCase useCase, [FromBody] LoginUseCaseModel model) =>
            {
                var response = await useCase.ExecuteAsync(model);
                
                return Results.Ok(response);
            });
        
        mapGroup.MapPost(
             "/", 
             [ProducesResponseType(typeof(UseCaseResult<Guid>), StatusCodes.Status201Created)]
             [AllowAnonymous] async ([FromBody] CreateUserInputModel model, [FromServices] ICreateUserUseCase userCase)  =>
        {
            var response = await userCase.ExecuteAsync(model);

            return Results.Created(url, response);
        }).AddEndpointFilter<ModelStateValidatorFilter<CreateUserInputModel>>();
        
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