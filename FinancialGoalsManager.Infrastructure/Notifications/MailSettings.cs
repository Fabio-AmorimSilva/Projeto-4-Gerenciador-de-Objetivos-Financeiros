namespace FinancialGoalsManager.Infrastructure.Notifications;

public sealed class MailSettings
{
    public string SmtpServer { get; init; } = null!;
    public int Port { get; init; }
}