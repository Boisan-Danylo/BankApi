using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Contracts
{
    public sealed class CreateAccountRequest
    {
        [Required] public string OwnerName { get; set; } = default!;
        [Range(0, double.MaxValue)] public decimal InitialBalance { get; set; }
        public string? Currency { get; set; } = "USD";
    }
}
