using Bank.Application.Interfaces;
using Bank.Application.Services;
using Bank.Domain;
using Bank.Domain.Accounts;
using Bank.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IAccountTransactionsService, AccountTransactionsService>();

var app = builder.Build();
app.UseSwagger(); app.UseSwaggerUI();
app.MapControllers();
app.Run();
public partial class Program { }