using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create type transaction_type as enum
        (
            'withdraw',
            'recharge'
        );

        create table transactions
        (
            transaction_id bigint primary key generated always as identity,
            user_account_number bigint not null,
            transaction_type transaction_type not null,
            balance int not null
        );

        create table admins
        (
            admin_id bigint primary key generated always as identity,
            admin_password text not null
        );

        create table users
        (
            user_account_number bigint primary key generated always as identity,
            user_password text not null,
            user_balance int not null
        );

        INSERT INTO admins VALUES (1, hello)
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table admins;
        drop table users;
        drop table transactions;
        drop table transactions_type;
        """;
}