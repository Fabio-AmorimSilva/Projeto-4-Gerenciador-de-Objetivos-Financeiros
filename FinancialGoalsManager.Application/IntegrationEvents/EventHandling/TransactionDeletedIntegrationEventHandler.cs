namespace FinancialGoalsManager.Application.IntegrationEvents.EventHandling;

public class TransactionDeletedIntegrationEventHandler(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService,
    IMailService mailService
) : IIntegrationEventHandler<TransactionDeletedIntegrationEvent>
{
    public async Task HandleAsync(TransactionDeletedIntegrationEvent @event)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            throw new NotFoundException(ErrorMessages.NotFound<User>());
        
        var transaction = await context.Users
            .AsNoTracking()
            .SelectMany(u => u.Transactions.Where(t => @event.TransactionId == t.Id))
            .FirstOrDefaultAsync(t => t.Id == @event.TransactionId);
        
        if(transaction is null)
            throw new NotFoundException(ErrorMessages.NotFound<Transaction>());
        
        var html = await File.ReadAllTextAsync(path: @"Templates\\DeleteTransaction\\DeleteTransactionTemplate.html");
        var body = html
            .Replace("{FinancialGoalName}", transaction.FinancialGoal.Title)
            .Replace("{UserName}", user.Name)
            .Replace("{CurrentDate}", DateTime.Today.ToShortDateString());
        
        await mailService.SendEmailAsync(
            from: user.Email,
            to: user.Email,
            subject: "Transaction Deleted",
            password: user.Password,
            body: body
        );
    }
}