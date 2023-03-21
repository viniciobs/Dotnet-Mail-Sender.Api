using FluentValidation;
using MailSender.Api;
using MailSender.Api.DTOs;
using MailSender.Api.Extensions;
using MailSender.Api.Services;
using MailSender.Api.Services.Impl;
using Microsoft.AspNetCore.Mvc;

#region Configuration

var builder = WebApplication.CreateBuilder(args);

var smtpSettings = builder.Configuration
    .GetSection("Smtp")
    .Get<SmtpSettings>();

if (smtpSettings is null)
{
    throw new ApplicationException("Missing smtp settings");
}

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton(smtpSettings)
    .AddScoped<IValidator<Email>, EmailValidator>()
    .AddScoped<IMailSender, EmailSender>();

var app = builder.Build();

app.
    UseHttpsRedirection()
    .ConfigureExceptionMiddleWare();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

#endregion Configuration

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("MailSender.Api");

app.MapPost("/send", async (
    HttpContext context,
    [FromServices] IValidator<Email> validator,
    [FromServices] IMailSender mailSender,
    [FromBody] Email data,
    CancellationToken cancellationToken) =>
{
    logger.LogInformation(data.SerializeWithTraceId(context.TraceIdentifier));
    
    var validationResult = await validator.ValidateAsync(data, cancellationToken);

    if (validationResult.IsValid is false)
    {
        logger.LogError(validationResult.Errors.SerializeErrorWithTraceId(context.TraceIdentifier));

        return Results.ValidationProblem(validationResult.FormatErrors());
    }

    var (isSuccess, error) = await mailSender.SendAsync(data, cancellationToken);

    if (isSuccess)
    {
        return Results.Ok();
    }

    if (error is not null)
    {
        logger.LogError(error.SerializeErrorWithTraceId(context.TraceIdentifier));
    }

    return Results.UnprocessableEntity(error);
})
.WithName("Send")
.WithOpenApi();

app.Run();