namespace FinancialGoalsManager.Infrastructure.Notifications.MailHandling;

public class MailHandlingService(IOptions<MailSettings> options) : IMailService
{
    public async Task<string> SendEmailAsync(string from, string to, string password, string subject, string body)
    {
        try
        {
            var settings = options.Value;
            var smtpServer = settings.SmtpServer;
            var port = settings.Port;

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(EmailConst.From, from));
            email.From.Add(new MailboxAddress(EmailConst.To, to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(from, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return EmailConst.SendMessage;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}