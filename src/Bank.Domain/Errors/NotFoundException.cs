using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Errors;
public sealed class NotFoundException(string message) : Exception(message);