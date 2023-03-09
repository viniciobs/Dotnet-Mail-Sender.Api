using FluentValidation.Results;

namespace MailSender.Api.Extensions
{
    public static  class ValidationResultErrorExtension
    {
        public static Dictionary<string, string[]> FormatErrors(
            this ValidationResult validationResult) =>
                validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Select(y => y.ErrorMessage).ToArray()
                    );
    }
}
