namespace Application.Models;

public record Transaction(long AccountNumber, TransactionType Type, int CurrentBalance);