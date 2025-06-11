namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.AddFinancialGoal;

public sealed record AddFinancialGoalInputModel
{
    public string Title { get; init; } = null!;
    public decimal Goal { get; init; }
    public DateTime DueDate { get; init; }
    public decimal? MonthGoal { get; init; }
    public GoalStatus Status { get; init; }
}

public class AddFinancialGoalInputModelValidator : AbstractValidator<AddFinancialGoalInputModel>
{
    public AddFinancialGoalInputModelValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddFinancialGoalInputModel.Title)))
            .MaximumLength(FinancialGoal.TitleMaxLength)
            .WithMessage(ErrorMessages.HasMaxLength(nameof(AddFinancialGoalInputModel.Title), FinancialGoal.TitleMaxLength));

        RuleFor(model => model.Goal)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddFinancialGoalInputModel.Goal)));

        RuleFor(model => model.DueDate)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddFinancialGoalInputModel.DueDate)));

        RuleFor(model => model.MonthGoal)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddFinancialGoalInputModel.MonthGoal)));

        RuleFor(model => model.Status)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddFinancialGoalInputModel.Status)));
    }
}