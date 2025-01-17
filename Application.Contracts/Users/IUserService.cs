using Application.Models;
using Application.Models.ResultTypes;

namespace Application.Contracts.Users;

public interface IUserService
{
    LoginResult Login(long accountNumber, string password);

    RechargeResult RechargeAccount(int amount);

    WithdrawResult WithdrawMoney(int amount);

    int ShowBalance();

    IList<Transaction> ShowOperationHistory();

    void Logout();
}