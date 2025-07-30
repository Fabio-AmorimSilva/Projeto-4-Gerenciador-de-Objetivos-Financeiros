namespace FinancialGoalsManager.Application.IntegrationEvents.EventHandling;

public class TransactionDeletedIntegrationEventHandler(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService,
    IMailService mailService
) : IIntegrationEventHandler<TransactionDeletedIntegrationEvent>
{
    public async Task HandleAsync(TransactionDeletedIntegrationEvent @event)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            throw new NotFoundException(ErrorMessages.NotFound<User>());
        
        var transaction = await context.Users
            .SelectMany(u => u.Transactions.Where(t => @event.TransactionId == t.Id))
            .FirstOrDefaultAsync(t => t.Id == @event.TransactionId);
        
        if(transaction is null)
            throw new NotFoundException(ErrorMessages.NotFound<Transaction>());
        
        await mailService.SendEmailAsync(
            from: user.Email,
            to: user.Email,
            subject: "Transaction Deleted",
            password: user.Password,
            body: "Sended"
        );
    }
}