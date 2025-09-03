using Bank.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Interfaces
{

    public interface IAccountService
    {
        Account Create(string ownerName, decimal initialBalance, string currency = "USD");
        Account Get(string accountNumber);
        IReadOnlyCollection<Account> List();
    }
}
