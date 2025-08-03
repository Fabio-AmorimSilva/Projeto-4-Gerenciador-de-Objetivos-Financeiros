namespace FinancialGoalsManager.Application.IntegrationEvents.EventHandling;

public sealed class FinancialGoalCreatedIntegrationEventHandler(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService,
    IMailService mailService
): IIntegrationEventHandler<FinancialGoalCreatedIntegrationEvent>
{
    public async Task HandleAsync(FinancialGoalCreatedIntegrationEvent @event)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);
        
        if (user is null)
            throw new NotFoundException(ErrorMessages.NotFound<User>());

        var html = await File.ReadAllTextAsync(path: @"Templates\\CreateFinancialGoal\\CreateFinancialGoalTemplate.html");
        var body = html
            .Replace("{FinancialGoalName}", @event.Title)
            .Replace("{UserName}", user.Name)
            .Replace("{CurrentDate}", DateTime.Today.ToShortDateString());

        await mailService.SendEmailAsync(
            from: user.Email,
            to: user.Email,
            subject: "Financial goal created",
            password: user.Password,
            body: body
        );
    }
}