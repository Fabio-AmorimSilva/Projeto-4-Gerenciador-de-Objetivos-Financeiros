namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.UpdateFinancialGoal;

public sealed record UpdateFinancialGoalUseCaseInputModel
{
    public string Title { get; set; } = null!;
    public decimal Goal { get; set; }
    public DateTime DueDate { get; set; }
    public decimal? MonthGoal { get; set; }
}

public class UpdateFinancialGoalUseCaseOutputModelValidator : AbstractValidator<UpdateFinancialGoalUseCaseInputModel>
{
    public UpdateFinancialGoalUseCaseOutputModelValidator()
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
    }
}