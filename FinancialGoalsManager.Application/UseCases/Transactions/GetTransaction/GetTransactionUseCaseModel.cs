namespace FinancialGoalsManager.Application.UseCases.Transactions.GetTransaction;

public sealed record GetTransactionUseCaseModel
{
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}