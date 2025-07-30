namespace FinancialGoalsManager.Application.IntegrationEvents.EventHandling;

public class TransactionCreatedIntegrationEventHandler(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService,
    IMailService mailService
) : IIntegrationEventHandler<TransactionCreatedIntegrationEvent>
{
    public async Task HandleAsync(TransactionCreatedIntegrationEvent @event)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            throw new NotFoundException(ErrorMessages.NotFound<User>());
        
        await mailService.SendEmailAsync(
            from: user.Email,
            to: user.Email,
            subject: "Transaction created",
            password: user.Password,
            body: "Sended"
        );
    }
}