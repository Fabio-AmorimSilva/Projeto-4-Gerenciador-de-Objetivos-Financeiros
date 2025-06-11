namespace FinancialGoalsManager.Application.UseCases.Users.UpdateUser;

public sealed record UpdateUserInputModel
{
    public required Guid UserId { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public class UpdateUserInputModelValidator : AbstractValidator<UpdateUserInputModel>
{
    public UpdateUserInputModelValidator()
    {
        RuleFor(model => model.UserId)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(UpdateUserInputModel.UserId)));
        
        RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(UpdateUserInputModel.Name)))
            .MaximumLength(User.MaxNameLength)
            .WithMessage(ErrorMessages.HasMaxLength( nameof(UpdateUserInputModel.Name), User.MaxNameLength));
        
        RuleFor(model => model.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(UpdateUserInputModel.Email)));
        
        RuleFor(model => model.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(UpdateUserInputModel.Password)));
    }
}