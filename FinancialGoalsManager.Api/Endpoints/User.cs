namespace FinancialGoalsManager.Api.Endpoints;

public static class User
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("/api/users")
            .RequireAuthorization();
        
        const string url = "/api/users";
        
        mapGroup.MapGet("/",[AllowAnonymous] async (AddUserInputModel model, IAddUserUseCase userCase)  =>
        {
            var userId = await userCase.ExecuteAsync(model);

            return Results.Created(url, userId);
        });
        
        mapGroup.MapPut("{userId:guid}/update", async (UpdateUserInputModel model, IUpdateUserUseCase userCase)  =>
        {
            await userCase.ExecuteAsync(model);

            return Results.NoContent();
        });

        mapGroup.MapDelete("{userId:guid}/delete", async (DeleteUserInputModel model, IDeleteUserUseCase userCase) =>
        {
            await userCase.ExecuteAsync(model);

            return Results.NoContent();
        });
    }
}