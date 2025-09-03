using Bank.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Interfaces
{
    public interface IAccountTransactionsService
    {
        Account Deposit(string accountNumber, decimal amount);
        Account Withdraw(string accountNumber, decimal amount);
        (Account From, Account To) Transfer(string fromAccount, string toAccount, decimal amount);
    }
}
