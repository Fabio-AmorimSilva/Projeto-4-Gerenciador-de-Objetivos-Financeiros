namespace FinancialGoalsManager.Application.UseCases.FinancialGoals.DeleteFinancialGoal;

public sealed record DeleteFinancialGoalUseCaseInputModel
{
    public Guid FinancialGoalId { get; set; }
}

public class DeleteFinancialGoalUseCaseInputModelValidator : AbstractValidator<DeleteFinancialGoalUseCaseInputModel>
{
    public DeleteFinancialGoalUseCaseInputModelValidator()
    {
        RuleFor(model => model.FinancialGoalId)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(DeleteFinancialGoalUseCaseInputModel.FinancialGoalId)));
    }
}