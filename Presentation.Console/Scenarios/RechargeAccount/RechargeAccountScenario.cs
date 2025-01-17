using Application.Contracts.Users;
using Application.Models.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Scenarios.RechargeAccount;

public class RechargeAccountScenario : IScenario
{
    private readonly IUserService _userService;

    public RechargeAccountScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Recharge Account";

    public void Run()
    {
        int amount = AnsiConsole.Ask<int>("Enter amount to recharge account");

        RechargeResult result = _userService.RechargeAccount(amount);

        string message = result switch
        {
            RechargeResult.Success => "Successful recharging account",
            RechargeResult.Failure => "Invalid amount for recharging account",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}