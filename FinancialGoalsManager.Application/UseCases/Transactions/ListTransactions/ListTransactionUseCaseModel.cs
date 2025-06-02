namespace FinancialGoalsManager.Application.UseCases.Transactions.ListTransactions;

public sealed record ListTransactionUseCaseModel
{
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}