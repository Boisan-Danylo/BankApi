using Bank.Domain.Accounts;

namespace Bank.Application.Interfaces;

public interface IAccountService
{
    Account Create(string ownerName, decimal initialBalance, string currency = "USD");
    Account Get(string accountNumber);
    IReadOnlyCollection<Account> List();
}