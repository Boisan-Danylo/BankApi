namespace Bank.Domain.Errors;

public sealed class ValidationException(string message) : Exception(message);