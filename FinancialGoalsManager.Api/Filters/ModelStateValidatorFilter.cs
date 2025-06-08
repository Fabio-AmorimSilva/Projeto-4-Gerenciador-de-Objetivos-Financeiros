namespace FinancialGoalsManager.Api.Filters;

public sealed class ModelStateValidatorFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();
        var useCase = context.Arguments
            .OfType<T>()
            .FirstOrDefault(a => a.GetType() == typeof(T));

        if (useCase is not null)
        {
            var validationResult = await validator.ValidateAsync(useCase);
            if (validationResult.IsValid)
                return await next(context);
            
            return Results.UnprocessableEntity(error: new UnprocessableResponse<T>(validationResult.Errors.First().ErrorMessage));
        }
        return await next(context);
    }
}