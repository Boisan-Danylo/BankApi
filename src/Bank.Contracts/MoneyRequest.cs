using System.ComponentModel.DataAnnotations;

namespace Bank.Contracts;

public sealed class MoneyRequest
{
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
}