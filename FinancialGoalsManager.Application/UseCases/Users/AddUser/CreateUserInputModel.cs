namespace FinancialGoalsManager.Application.UseCases.Users.AddUser;

public sealed record CreateUserInputModel
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public class AddUserInputModelValidator : AbstractValidator<CreateUserInputModel>
{
    public AddUserInputModelValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateUserInputModel.Name)))
            .MaximumLength(User.MaxNameLength)
            .WithMessage(ErrorMessages.HasMaxLength(nameof(CreateUserInputModel.Name), User.MaxNameLength));
        
        RuleFor(m => m.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateUserInputModel.Email)));
        
        RuleFor(m => m.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateUserInputModel.Password)));
    }
}