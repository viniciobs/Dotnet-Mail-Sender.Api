using System.Text;

namespace MailSender.Api.DTOs
{
    public record Email(
        string To,
        string Subject,
        string Body,
        bool IsHtml);
}
