using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Domain.Errors;

namespace Bank.Domain.Accounts
{

    public sealed class Account
    {
        public string AccountNumber { get; init; } = default!;
        public string OwnerName { get; init; } = default!;
        public string Currency { get; init; } = "USD";
        public decimal Balance { get; private set; }

        internal object Sync { get; } = new();

        public Account(string number, string owner, string currency, decimal initial)
        {
            if (initial < 0) throw new ValidationException("Initial balance cannot be negative.");
            AccountNumber = number;
            OwnerName = owner;
            Currency = string.IsNullOrWhiteSpace(currency) ? "USD" : currency;
            Balance = initial;
        }

        internal void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ValidationException("Amount must be > 0");
            Balance += amount;
        }

        internal void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ValidationException("Amount must be > 0");
            if (Balance < amount) throw new InsufficientFundsException(AccountNumber, Balance, amount);
            Balance -= amount;
        }
    }
}
