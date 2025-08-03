using System.Globalization;

namespace FinancialGoalsManager.Application.IntegrationEvents.EventHandling;

public class TransactionCreatedIntegrationEventHandler(
    IFinancialGoalManagerDbContext context,
    IRequestContextService requestContextService,
    IMailService mailService
) : IIntegrationEventHandler<TransactionCreatedIntegrationEvent>
{
    public async Task HandleAsync(TransactionCreatedIntegrationEvent @event)
    {
        var user = await context.Users
            .AsNoTracking()
            .Include(u => u.FinancialGoals.Where(f => f.Id == @event.FinancialGoalId))
            .FirstOrDefaultAsync(u => u.Id == requestContextService.UserId);

        if (user is null)
            throw new NotFoundException(ErrorMessages.NotFound<User>());
        
        var financialGoal = user.GetGoal(financialGoalId: @event.FinancialGoalId);
        if (financialGoal is null)
            throw new NotFoundException(ErrorMessages.NotFound<FinancialGoal>());
        
        var html = await File.ReadAllTextAsync(path: @"Templates\\CreateTransaction\\CreateTransactionTemplate.html");
        var body = html
            .Replace("{FinancialGoalName}", financialGoal.Title)
            .Replace("{Quantity}", @event.Quantity.ToString(CultureInfo.InvariantCulture))
            .Replace("{UserName}", user.Name)
            .Replace("{CurrentDate}", DateTime.Today.ToShortDateString());
        
        await mailService.SendEmailAsync(
            from: user.Email,
            to: user.Email,
            subject: "Transaction created",
            password: user.Password,
            body: body
        );
    }
}