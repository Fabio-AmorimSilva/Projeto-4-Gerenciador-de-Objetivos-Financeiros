namespace FinancialGoalsManager.Api.Filters;

public sealed class ModelStateValidatorFilter(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;
            
            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = serviceProvider.GetService(validatorType) as IValidator;

            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument));

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    context.Result = new BadRequestObjectResult(new Error
                    {
                        StatusCode = 422,
                        Message = errors.First()
                    });
                    
                    return;
                }
            }
        }
        
        await next();
    }
}