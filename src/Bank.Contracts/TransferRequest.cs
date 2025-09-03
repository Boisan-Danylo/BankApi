using System.ComponentModel.DataAnnotations;

namespace Bank.Contracts;

public sealed class TransferRequest
{
    [Required] public string FromAccount { get; set; } = null!;
    [Required] public string ToAccount { get; set; } = null!;
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
}