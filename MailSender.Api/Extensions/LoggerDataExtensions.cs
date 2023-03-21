using System.Text.Json;

namespace MailSender.Api.Extensions
{
    public static class LoggerDataExtensions
    {
        public static string SerializeWithTraceId(this object data, string traceId) =>
            JsonSerializer.Serialize(
                new
                {
                    TraceId = traceId,
                    Datetime = DateTime.UtcNow,
                    Data = data,
                });

        public static string SerializeErrorWithTraceId(this object error, string traceId) =>
            JsonSerializer.Serialize(
                new
                {
                    TraceId = traceId,
                    Datetime = DateTime.UtcNow,
                    Error = error,
                });
    }
}
