using System.Collections.Concurrent;
using Bank.Application.Interfaces;
using Bank.Domain.Accounts;
using Bank.Domain.Errors;

namespace Bank.Application.Services;

public sealed class AccountTransactionsService(IAccountRepository repo) : IAccountTransactionsService
{
    private static readonly ConcurrentDictionary<string, object> _locks = new();

    public Account Deposit(string accountNumber, decimal amount)
    {
        var acc = repo.Get(accountNumber);
        var l = _locks.GetOrAdd(accountNumber, _ => new object());

        lock (l)
        {
            acc.Deposit(amount);
            repo.Update(acc);
            return acc;
        }
    }

    public Account Withdraw(string accountNumber, decimal amount)
    {
        var acc = repo.Get(accountNumber);
        var l = _locks.GetOrAdd(accountNumber, _ => new object());

        lock (l)
        {
            acc.Withdraw(amount);
            repo.Update(acc);
            return acc;
        }
    }

    public (Account From, Account To) Transfer(string fromAccount, string toAccount, decimal amount)
    {
        if (fromAccount == toAccount)
            throw new ValidationException("Cannot transfer to the same account.");
        if (amount <= 0)
            throw new ValidationException("Amount must be > 0");

        var a = repo.Get(fromAccount);
        var b = repo.Get(toAccount);

        if (!string.Equals(a.Currency, b.Currency, StringComparison.OrdinalIgnoreCase))
            throw new ValidationException("Currency mismatch.");

        var firstKey = string.CompareOrdinal(fromAccount, toAccount) < 0 ? fromAccount : toAccount;
        var secondKey = ReferenceEquals(firstKey, fromAccount) ? toAccount : fromAccount;

        var firstLock = _locks.GetOrAdd(firstKey, _ => new object());
        var secondLock = _locks.GetOrAdd(secondKey, _ => new object());

        lock (firstLock)
        lock (secondLock)
        {
            if (firstKey == fromAccount)
            {
                a.Withdraw(amount);
                b.Deposit(amount);
            }
            else
            {
                b.Withdraw(amount);
                a.Deposit(amount);
            }

            repo.Update(a);
            repo.Update(b);
            return (a, b);
        }
    }
}