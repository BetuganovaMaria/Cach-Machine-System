using Application.Contracts.Users;
using Application.Models.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Login.LoginUser;

public class LoginUserScenario : IScenario
{
    private readonly IUserService _userService;

    public LoginUserScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Login User";

    public void Run()
    {
        long accountNumber = AnsiConsole.Ask<long>("Enter your account number");
        string password = AnsiConsole.Ask<string>("Enter your password");

        LoginResult result = _userService.Login(accountNumber, password);

        string message = result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.Failure(WrongPassword) => "Wrong password",
            LoginResult.Failure(NotFound) => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}