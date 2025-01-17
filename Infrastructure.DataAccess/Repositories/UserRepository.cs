using Application.Abstractions.Repositories;
using Application.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? FindUserByAccountNumber(long accountNumber)
    {
        const string sql = """
                           select user_account_number, user_password, user_balance
                           from users
                           where user_account_number = :accountNumber;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountNumber", accountNumber);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetInt32(2));
    }

    public void UpdateUserBalance(long accountNumber, int newBalance, TransactionType type)
    {
        const string sql = """
                           UPDATE users 
                           SET user_balance = :newBalance 
                           where user_account_number = :accountNumber;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("newBalance", newBalance)
            .AddParameter("accountNumber", accountNumber);

        command.ExecuteNonQuery();

        var transaction = new Transaction(accountNumber, type, newBalance);
        WriteTransaction(accountNumber, newBalance, transaction);
    }

    public IList<Transaction> GetUserTransactions(long accountNumber)
    {
        const string sql = """
                           SELECT transaction_id, user_account_number, transaction_type, balance 
                           FROM transactions 
                           WHERE user_account_number=:accountNumber 
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountNumber", accountNumber);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return [];

        command.ExecuteNonQuery();

        var transactions = new List<Transaction>();
        while (reader.Read())
        {
            transactions.Add(new Transaction(
                reader.GetInt64(0),
                reader.GetFieldValue<TransactionType>(1),
                reader.GetInt32(2)));
        }

        return transactions;
    }

    private void WriteTransaction(long accountNumber, int newBalance, Transaction transaction)
    {
        const string sql = """
                           INSERT INTO operations (transaction_id, user_account_number, transaction_type, balance) 
                           VALUES (:transactionId, :accountNumber, :transactionType, :balance)
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("transactionId", newBalance)
            .AddParameter("accountNumber", accountNumber)
            .AddParameter("transactionType", transaction.Type)
            .AddParameter("balance", transaction.CurrentBalance);

        command.ExecuteNonQuery();
    }
}