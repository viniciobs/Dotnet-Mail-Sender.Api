using FluentValidation;
using MailSender.Api.DTOs;
using MailSender.Api.Extensions;
using MailSender.Api.Services;
using MailSender.Api.Services.Impl;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddScoped<IValidator<Email>, EmailValidator>()
    .AddSingleton<IMailSender, EmailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/send", async (
    [FromServices] IValidator<Email> validator,
    [FromServices] IMailSender mailSender,
    [FromBody] Email data,
    CancellationToken cancellationToken) =>
{
    var validationResult = await validator.ValidateAsync(data, cancellationToken);

    if (validationResult.IsValid is false)
    {
        return Results.ValidationProblem(validationResult.FormatErrors());
    }

    await mailSender.SendAsync(data, cancellationToken);

    return Results.Ok();
})
.WithName("Send")
.WithOpenApi();

app.Run();