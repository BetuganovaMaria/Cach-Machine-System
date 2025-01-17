using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Logout.LogoutUser;

public class LogoutUserScenario : IScenario
{
    private readonly IUserService _userService;

    public LogoutUserScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Logout User";

    public void Run()
    {
        _userService.Logout();

        AnsiConsole.WriteLine("You have logged out");
        AnsiConsole.Ask<string>("Ok");
    }
}