namespace FinancialGoalsManager.Application.UseCases.Users.UpdatePassword;

public sealed record UpdatePasswordInputModel
{
    public required Guid UserId { get; init; }
    public required string NewPassword { get; init; }
}

public class UpdatePasswordInputModelValidator : AbstractValidator<UpdatePasswordInputModel>
{
    public UpdatePasswordInputModelValidator()
    {
        RuleFor(model => model.UserId)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(UpdatePasswordInputModel.UserId)));
        
        RuleFor(model => model.NewPassword)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(UpdatePasswordInputModel.NewPassword)));
    }
}