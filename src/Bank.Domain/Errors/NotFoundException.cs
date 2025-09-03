namespace Bank.Domain.Errors;

public sealed class NotFoundException(string message) : Exception(message);