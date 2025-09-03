using Bank.Application.Interfaces;
using Bank.Application.Services;
using Bank.Domain;
using Bank.Domain.Accounts;
using Bank.Domain.Errors;
using Bank.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IAccountTransactionsService, AccountTransactionsService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var ex = feature?.Error;

        var (status, title, detail) = ex switch
        {
            ValidationException ve => (StatusCodes.Status400BadRequest, "Validation error", ve.Message),
            NotFoundException => (StatusCodes.Status404NotFound, "Not found", ex!.Message),
            InsufficientFundsException => (StatusCodes.Status409Conflict, "Conflict", ex!.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error",
                                             app.Environment.IsDevelopment() ? ex?.Message : "Unexpected error")
        };

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problem);
    });
});

app.UseSwagger(); app.UseSwaggerUI();
app.MapControllers();
app.Run();
public partial class Program { }