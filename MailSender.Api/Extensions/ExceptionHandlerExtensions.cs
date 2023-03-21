using static System.Net.Mime.MediaTypeNames;

namespace MailSender.Api.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        private const string DefaultError = "Unable to send. Please try again.";

        public static IApplicationBuilder ConfigureExceptionMiddleWare(this IApplicationBuilder app) =>
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    context.Response.ContentType = Application.Json;

                    await context.Response.WriteAsync(DefaultError.SerializeErrorWithTraceId(context.TraceIdentifier));
                });
            });
    }
}
