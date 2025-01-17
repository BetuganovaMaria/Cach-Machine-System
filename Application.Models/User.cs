namespace Application.Models;

public record User(long AccountNumber, string Password, int Balance = 0);