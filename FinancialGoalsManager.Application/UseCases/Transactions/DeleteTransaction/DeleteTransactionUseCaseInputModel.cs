namespace FinancialGoalsManager.Application.UseCases.Transactions.DeleteTransaction;

public sealed record DeleteTransactionUseCaseInputModel
{
    public Guid TransactionId { get; set; }
}

public class DeleteTransactionUseCaseInputModelValidator : AbstractValidator<DeleteTransactionUseCaseInputModel>
{
    public DeleteTransactionUseCaseInputModelValidator()
    {
        RuleFor(useCase => useCase.TransactionId)
            .NotEmpty()
            .WithMessage(ErrorMessages.CannotBeEmpty(nameof(DeleteTransactionUseCaseInputModel.TransactionId)));
    }
}