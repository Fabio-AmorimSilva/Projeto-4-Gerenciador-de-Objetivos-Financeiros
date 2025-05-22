namespace FinancialGoalsManager.Api.Filters;

public sealed class ModelStateValidatorFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .SelectMany(modelState =>
                    modelState.Value?.Errors.Select(e => e.ErrorMessage) ??
                    Array.Empty<string>()
                );

            var error = new Error
            {
                StatusCode = 422,
                Message = errors.First()
            };

            context.HttpContext.Response.StatusCode = error.StatusCode;
            await context.HttpContext.Response.WriteAsJsonAsync(error);
            
            await next();
        }
    }
}