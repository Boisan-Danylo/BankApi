namespace Bank.Domain.Errors;

public sealed class InsufficientFundsException(string acc, decimal balance, decimal needed)
    : Exception($"Insufficient funds on {acc}. Balance={balance}, needed={needed}");