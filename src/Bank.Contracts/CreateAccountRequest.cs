using System.ComponentModel.DataAnnotations;

namespace Bank.Contracts;

public sealed class CreateAccountRequest
{
    [Required] public string OwnerName { get; set; } = null!;
    [Range(0, double.MaxValue)] public decimal InitialBalance { get; set; }
    public string? Currency { get; set; } = "USD";
}