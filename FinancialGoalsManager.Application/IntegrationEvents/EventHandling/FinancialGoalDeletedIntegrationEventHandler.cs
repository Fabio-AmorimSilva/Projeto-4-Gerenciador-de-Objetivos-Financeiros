namespace FinancialGoalsManager.Application.IntegrationEvents.EventHandling;

public sealed class FinancialGoalDeletedIntegrationEventHandler(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService,
    IMailService mailService    
) : IIntegrationEventHandler<FinancialGoalDeletedIntegrationEvent>
{
    public async Task HandleAsync(FinancialGoalDeletedIntegrationEvent @event)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            throw new NotFoundException(ErrorMessages.NotFound<User>());

        var financialGoal = await context.Users
            .SelectMany(u => u.FinancialGoals.Where(t => @event.FinancialGoalId == t.Id))
            .FirstOrDefaultAsync(t => t.Id == @event.FinancialGoalId);
        
        if(financialGoal is null)
            throw new NotFoundException(ErrorMessages.NotFound<FinancialGoal>());
        
        await mailService.SendEmailAsync(
            from: user.Email,
            to: user.Email,
            subject: "Financial goal Deleted",
            password: user.Password,
            body: "Sended"
        );
    }
}