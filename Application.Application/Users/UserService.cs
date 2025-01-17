using Application.Abstractions.Repositories;
using Application.Contracts.Users;
using Application.Models;
using Application.Models.ResultTypes;

namespace Application.Application.Users;

internal class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly CurrentUserManager _currentUserManager;

    public UserService(IUserRepository repository, CurrentUserManager currentUserManager)
    {
        _repository = repository;
        _currentUserManager = currentUserManager;
    }

    public LoginResult Login(long accountNumber, string password)
    {
        User? user = _repository.FindUserByAccountNumber(accountNumber);

        if (user is null)
        {
            return new LoginResult.Failure(new NotFound());
        }

        if (user.Password != password)
        {
            return new LoginResult.Failure(new WrongPassword());
        }

        _currentUserManager.User = user;
        return new LoginResult.Success();
    }

    public RechargeResult RechargeAccount(int amount)
    {
        if (amount <= 0)
            return new RechargeResult.Failure();

        if (_currentUserManager.User is null)
            return new RechargeResult.Failure();

        _currentUserManager.User =
            _currentUserManager.User with { Balance = _currentUserManager.User.Balance + amount };

        _repository.UpdateUserBalance(
            _currentUserManager.User.AccountNumber,
            _currentUserManager.User.Balance,
            TransactionType.Recharge);

        return new RechargeResult.Success();
    }

    public WithdrawResult WithdrawMoney(int amount)
    {
        if (amount <= 0)
            return new WithdrawResult.Failure();

        if (_currentUserManager.User is null)
            return new WithdrawResult.Failure();

        if (_currentUserManager.User.Balance < amount)
            return new WithdrawResult.Failure();

        _currentUserManager.User =
            _currentUserManager.User with { Balance = _currentUserManager.User.Balance - amount };

        _repository.UpdateUserBalance(
            _currentUserManager.User.AccountNumber,
            _currentUserManager.User.Balance,
            TransactionType.Withdraw);

        return new WithdrawResult.Success();
    }

    public int ShowBalance()
    {
        if (_currentUserManager.User is null)
            return 0;

        return _currentUserManager.User.Balance;
    }

    public IList<Transaction> ShowOperationHistory()
    {
        if (_currentUserManager.User is null)
            return [];

        return _repository.GetUserTransactions(_currentUserManager.User.AccountNumber);
    }

    public void Logout()
    {
        _currentUserManager.User = null;
    }
}