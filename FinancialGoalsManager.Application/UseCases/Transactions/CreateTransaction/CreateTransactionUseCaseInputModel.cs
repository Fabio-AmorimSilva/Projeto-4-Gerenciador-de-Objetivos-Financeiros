namespace FinancialGoalsManager.Application.UseCases.Transactions.CreateTransaction;

public sealed record CreateTransactionUseCaseInputModel
{
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}

public class CreateTransactionUseCaseInputModelValidator : AbstractValidator<CreateTransactionUseCaseInputModel>
{
    public CreateTransactionUseCaseInputModelValidator()
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