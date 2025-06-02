namespace FinancialGoalsManager.Application.UseCases.Transactions.AddTransaction;

public sealed record AddTransactionUseCaseInputModel
{
    public decimal Quantity { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}