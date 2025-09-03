namespace Bank.Contracts;

public sealed record AccountDto(string AccountNumber, string OwnerName, string Currency, decimal Balance);