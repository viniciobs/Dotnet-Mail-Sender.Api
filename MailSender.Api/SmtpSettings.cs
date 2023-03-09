namespace MailSender.Api
{
    public record SmtpSettings(
        string Host,
        int Port,
        string Username,
        string Password);
}
