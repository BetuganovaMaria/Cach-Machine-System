using Application.Contracts.Users;
using Application.Models.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Scenarios.WithdrawMoney;

public class WithdrawMoneyScenario : IScenario
{
    private readonly IUserService _userService;

    public WithdrawMoneyScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Withdraw Money";

    public void Run()
    {
        int amount = AnsiConsole.Ask<int>("Enter amount to withdraw");

        WithdrawResult result = _userService.WithdrawMoney(amount);

        string message = result switch
        {
            WithdrawResult.Success => "Successful withdrawing money",
            WithdrawResult.Failure => "Invalid amount for withdrawing money",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}