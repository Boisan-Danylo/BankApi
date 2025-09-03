using System.Collections.Concurrent;
using Bank.Domain.Accounts;
using Bank.Domain.Errors;

namespace Bank.Infrastructure;

// CRUD Operations for storing and retrieving Account entities
// Imitating a database repository
// In a real-world application, this would be an interface with a database context
public sealed class InMemoryAccountRepository : IAccountRepository
{
    private readonly ConcurrentDictionary<string, Account> _store = new();

    public Account Add(Account account)
    {
        if (!_store.TryAdd(account.AccountNumber, account))
            throw new InvalidOperationException("Account number collision.");
        return account;
    }

    public Account Get(string number)
    {
        return _store.TryGetValue(number, out var acc) ? acc : throw new NotFoundException("Account not found.");
    }

    public IReadOnlyCollection<Account> List()
    {
        return _store.Values.ToArray();
    }

    public void Update(Account account)
    {
        // Empty. Would be used if we have a real database
    }
}