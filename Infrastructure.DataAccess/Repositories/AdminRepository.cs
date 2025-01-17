using Application.Abstractions.Repositories;
using Application.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public Admin? FindAdminByPassword(string password)
    {
        const string sql = """
                           select admin_password
                           from admins
                           where admin_password = :password;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("password", password);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Admin(
            reader.GetInt64(0),
            reader.GetString(1));
    }

    public void UpdateSystemPassword(string newPassword)
    {
        const string sql = """
                           UPDATE admins 
                           SET admin_password = :newPassword;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("newPassword", newPassword);

        command.ExecuteNonQuery();
    }

    public void RegisterUser(long accountNumber, string password)
    {
        const string sql = """
                           INSERT INTO 
                           users (user_account_number, user_password, user_balance) 
                           VALUES (:accountNumber, :password, :balance)
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountNumber", accountNumber)
            .AddParameter("password", password)
            .AddParameter("balance", 0);

        command.ExecuteNonQuery();
    }
}