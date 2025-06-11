namespace FinancialGoalsManager.Application.UseCases.Transactions.AddTransaction;

public sealed record AddTransactionUseCaseInputModel
{
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}

public class AddFinancialGoalInputModelValidator : AbstractValidator<AddTransactionUseCaseInputModel>
{
    public AddFinancialGoalInputModelValidator()
    {
        RuleFor(model => model.Quantity)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddTransactionUseCaseInputModel.Quantity)));
        
        RuleFor(model => model.Type)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddTransactionUseCaseInputModel.Type)));
        
        RuleFor(model => model.Date)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(AddTransactionUseCaseInputModel.Date)));
    }
}