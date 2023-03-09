using MailSender.Api.DTOs;

namespace MailSender.Api.Services
{
    public interface IMailSender
    {
        Task SendAsync(Email data, CancellationToken cancellationToken);
    }
}
