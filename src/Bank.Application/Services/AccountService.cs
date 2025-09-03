using Bank.Application.Interfaces;
using Bank.Domain.Accounts;
using Bank.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Services
{
    public sealed class AccountService(IAccountRepository repo) : IAccountService
    {
        public Account Create(string ownerName, decimal initialBalance, string currency = "USD")
        {
            if (string.IsNullOrWhiteSpace(ownerName))
                throw new ValidationException("OwnerName is required.");
            if (initialBalance < 0)
                throw new ValidationException("Initial balance cannot be negative.");

            var number = Guid.NewGuid().ToString("N")[..12].ToUpperInvariant();
            var account = new Account(number, ownerName.Trim(), currency, initialBalance); repo.Add(account);
            return account;
        }

        public Account Get(string accountNumber) => 
            repo.Get(accountNumber);

        public IReadOnlyCollection<Account> List() =>
            repo.List().OrderBy(a => a.AccountNumber).ToArray();
    }
}
