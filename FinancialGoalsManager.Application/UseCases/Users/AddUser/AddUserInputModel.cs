namespace FinancialGoalsManager.Application.UseCases.Users.AddUser;

public sealed record AddUserInputModel
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public class AddUserInputModelValidator : AbstractValidator<AddUserInputModel>
{
    public AddUserInputModelValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddUserInputModel.Name)))
            .MaximumLength(User.MaxNameLength)
            .WithMessage(ErrorMessages.HasMaxLength(nameof(AddUserInputModel.Name), User.MaxNameLength));
        
        RuleFor(m => m.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddUserInputModel.Email)));
        
        RuleFor(m => m.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddUserInputModel.Password)));
    }
}