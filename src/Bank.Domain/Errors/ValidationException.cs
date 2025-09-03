using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Errors;
public sealed class ValidationException(string message) : Exception(message);
