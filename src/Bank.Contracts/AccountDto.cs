using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Contracts
{
    public sealed record AccountDto(string AccountNumber, string OwnerName, string Currency, decimal Balance);
}
