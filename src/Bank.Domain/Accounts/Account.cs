using Bank.Domain.Errors;

namespace Bank.Domain.Accounts;

public sealed class Account
{
    public Account(string number, string owner, string currency, decimal initial)
    {
        if (initial < 0) throw new ValidationException("Initial balance cannot be negative.");
        AccountNumber = number;
        OwnerName = owner;
        Currency = string.IsNullOrWhiteSpace(currency) ? "USD" : currency;
        Balance = initial;
    }

    public string AccountNumber { get; init; }
    public string OwnerName { get; init; }
    public string Currency { get; init; }
    public decimal Balance { get; private set; }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ValidationException("Amount must be > 0");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ValidationException("Amount must be > 0");
        if (Balance < amount) throw new InsufficientFundsException(AccountNumber, Balance, amount);
        Balance -= amount;
    }
}