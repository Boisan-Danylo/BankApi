using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Errors;
public sealed class InsufficientFundsException(string acc, decimal balance, decimal needed)
     : Exception($"Insufficient funds on {acc}. Balance={balance}, needed={needed}");

