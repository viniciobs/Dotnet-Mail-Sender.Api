using MailSender.Api.DTOs;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace MailSender.Api.Services.Impl
{
    public sealed class EmailSender : IMailSender
    {
        private readonly string _fromEmail;
        private readonly SmtpClient _client;
        private string? error;

        public EmailSender(SmtpSettings smtpSettings)
        {
            _client = new SmtpClient(smtpSettings.Host, smtpSettings.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    smtpSettings.Username,
                    smtpSettings.Password),
                EnableSsl = true
            };

            _client.SendCompleted += new SendCompletedEventHandler(EmailSentCallback);

            _fromEmail = smtpSettings.Username;
        }

        private void EmailSentCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                error = "Operation cancelled";
            }

            if (e.Error is not null)
            {
                error = e.Error.ToString();
            }
        }

        public async Task<(bool IsSuccess, string? Error)> SendAsync(Email data, CancellationToken cancellationToken)
        {
            await _client.SendMailAsync(
                new MailMessage(_fromEmail, data.To, data.Subject, data.Body)
                {
                    IsBodyHtml = data.IsHtml
                }, cancellationToken);

            return (IsSuccess: error is null, Error: error);
        }
    }
}
