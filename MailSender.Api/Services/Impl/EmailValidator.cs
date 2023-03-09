using FluentValidation;
using MailSender.Api.DTOs;

namespace MailSender.Api.Services.Impl
{
    public class EmailValidator : AbstractValidator<Email>
    {
        private const int SubjectMaxLength = 998;

        public EmailValidator()
        {
            RuleFor(x => x.From)
                .NotEmpty().WithMessage("Sender email address is required")
                .EmailAddress().WithMessage("Sender email address is not valid");

            RuleFor(x => x.To)
                .NotEmpty().WithMessage("Receiver email address is required")
                .EmailAddress().WithMessage("Receiver email address is not valid");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required")
                .MaximumLength(SubjectMaxLength).WithMessage($"Subject must contain up to {SubjectMaxLength} characters");

            RuleFor(x => x.Body)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Body is required");
        }
    }
}
