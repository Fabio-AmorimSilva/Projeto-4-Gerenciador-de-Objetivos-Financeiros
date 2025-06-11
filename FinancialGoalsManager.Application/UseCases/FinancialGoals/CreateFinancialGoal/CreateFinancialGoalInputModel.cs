namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.AddFinancialGoal;

public sealed record CreateFinancialGoalInputModel
{
    public string Title { get; init; } = null!;
    public decimal Goal { get; init; }
    public DateTime DueDate { get; init; }
    public decimal? MonthGoal { get; init; }
    public GoalStatus Status { get; init; }
}

public class AddFinancialGoalInputModelValidator : AbstractValidator<CreateFinancialGoalInputModel>
{
    public AddFinancialGoalInputModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateFinancialGoalInputModel.Title)))
            .MaximumLength(FinancialGoal.TitleMaxLength)
            .WithMessage(ErrorMessages.HasMaxLength(nameof(CreateFinancialGoalInputModel.Title), FinancialGoal.TitleMaxLength));

        RuleFor(model => model.Goal)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateFinancialGoalInputModel.Goal)));

        RuleFor(model => model.DueDate)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateFinancialGoalInputModel.DueDate)));

        RuleFor(model => model.MonthGoal)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateFinancialGoalInputModel.MonthGoal)));

        RuleFor(model => model.Status)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateFinancialGoalInputModel.Status)));
    }
}