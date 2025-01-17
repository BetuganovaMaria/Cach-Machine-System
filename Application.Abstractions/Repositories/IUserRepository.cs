using Application.Models;

namespace Application.Abstractions.Repositories;

public interface IUserRepository
{
    User? FindUserByAccountNumber(long accountNumber);

    void UpdateUserBalance(long accountNumber, int newBalance, TransactionType type);

    IList<Transaction> GetUserTransactions(long accountNumber);
}