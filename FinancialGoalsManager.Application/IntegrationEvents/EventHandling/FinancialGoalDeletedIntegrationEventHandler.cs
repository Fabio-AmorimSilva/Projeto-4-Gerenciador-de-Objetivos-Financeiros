namespace FinancialGoalsManager.Application.IntegrationEvents.EventHandling;

public sealed class FinancialGoalDeletedIntegrationEventHandler(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService,
    IMailService mailService    
) : IIntegrationEventHandler<FinancialGoalDeletedIntegrationEvent>
{
    public async Task HandleAsync(FinancialGoalDeletedIntegrationEvent @event)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            throw new NotFoundException(ErrorMessages.NotFound<User>());

        var financialGoal = await context.Users
            .AsNoTracking()
            .SelectMany(u => u.FinancialGoals.Where(t => @event.FinancialGoalId == t.Id))
            .FirstOrDefaultAsync(t => t.Id == @event.FinancialGoalId);
        
        if(financialGoal is null)
            throw new NotFoundException(ErrorMessages.NotFound<FinancialGoal>());
        
        var html = await File.ReadAllTextAsync(path: @"Templates\\DeleteFinancialGoal\\DeleteFinancialGoalTemplate.html");
        var body = html
            .Replace("{FinancialGoalName}", financialGoal.Title)
            .Replace("{UserName}", user.Name)
            .Replace("{CurrentDate}", DateTime.Today.ToShortDateString());
        
        await mailService.SendEmailAsync(
            from: user.Email,
            to: user.Email,
            subject: "Financial goal Deleted",
            password: user.Password,
            body: body
        );
    }
}