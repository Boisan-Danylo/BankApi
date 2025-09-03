namespace Bank.Contracts;

public sealed record TransferResultDto(AccountDto From, AccountDto To);