using Bank.Contracts;
using Bank.Domain.Accounts;

namespace Bank.Api.Mappers
{
    public static class AccountMapping
    {
        public static AccountDto ToDto(this Account a) =>
            new(a.AccountNumber, a.OwnerName, a.Currency, a.Balance);
    }
}
