using MailSender.Api.DTOs;

namespace MailSender.Api.Services
{
    public interface IMailSender
    {
        Task<(bool IsSuccess, string? Error)> SendAsync(Email data, CancellationToken cancellationToken);
    }
}
