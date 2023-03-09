using MailSender.Api.DTOs;
using System.Net;
using System.Net.Mail;

namespace MailSender.Api.Services.Impl
{
    public sealed class EmailSender : IMailSender
    {
        private readonly SmtpClient _client;

        public EmailSender(IConfiguration configuration)
        {
            var smtpSettings = configuration
                .GetSection("Smtp")
                .Get<SmtpSettings>();

            if (smtpSettings is null)
            {
                throw new ApplicationException("Missing smtp settings");
            }

            _client = new SmtpClient(smtpSettings.Host, smtpSettings.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    smtpSettings.Username,
                    smtpSettings.Password),
                EnableSsl = true
            };
        }

        public  async Task SendAsync(Email data, CancellationToken cancellationToken)
        {
            await Task.Run(() => {
                _client.Send(
                    data.From,
                    data.To,
                    data.Subject,
                    data.Body);
            }, cancellationToken);
        }
    }
}
