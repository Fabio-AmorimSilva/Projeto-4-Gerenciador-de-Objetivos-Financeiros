namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.UpdateFinancialGoal;

public sealed record UpdateFinancialGoalUseCaseInputModel
{
    public string Title { get; set; } = null!;
    public decimal Goal { get; set; }
    public DateTime DueDate { get; set; }
    public decimal? MonthGoal { get; set; }
    public GoalStatus Status { get; set; }
}

public class UpdateFinancialGoalUseCaseOutputModelValidator : AbstractValidator<UpdateFinancialGoalUseCaseInputModel>
{
    public UpdateFinancialGoalUseCaseOutputModelValidator()
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