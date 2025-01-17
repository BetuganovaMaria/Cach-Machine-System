using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenarios.ShowBalance;

public class ShowBalanceScenario : IScenario
{
    private readonly IUserService _userService;

    public ShowBalanceScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Show Balance";

    public void Run()
    {
        int balance = _userService.ShowBalance();

        AnsiConsole.WriteLine($"Current balance is {balance}");
        AnsiConsole.Ask<string>("Ok");
    }
}