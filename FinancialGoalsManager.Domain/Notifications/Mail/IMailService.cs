namespace FinancialGoalsManager.Domain.Notifications.Mail;

public interface IMailService
{
    Task<string> SendEmailAsync(string from, string to, string password, string subject, string body);
}