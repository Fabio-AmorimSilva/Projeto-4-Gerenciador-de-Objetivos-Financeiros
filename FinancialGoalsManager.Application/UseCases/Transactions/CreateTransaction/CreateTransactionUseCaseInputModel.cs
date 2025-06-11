namespace FinancialGoalsManager.Application.UseCases.Transactions.AddTransaction;

public sealed record CreateTransactionUseCaseInputModel
{
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}

public class AddFinancialGoalInputModelValidator : AbstractValidator<CreateTransactionUseCaseInputModel>
{
    public AddFinancialGoalInputModelValidator()
    {
        RuleFor(model => model.Quantity)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateTransactionUseCaseInputModel.Quantity)));
        
        RuleFor(model => model.Type)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateTransactionUseCaseInputModel.Type)));
        
        RuleFor(model => model.Date)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(CreateTransactionUseCaseInputModel.Date)));
    }
}