using Application.Contracts.Admins;
using Application.Models.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Login.LoginAdmin;

public class LoginAdminScenario : IScenario
{
    private readonly IAdminService _adminService;

    public LoginAdminScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Login Admin";

    public void Run()
    {
        string password = AnsiConsole.Ask<string>("Enter system password");

        LoginResult result = _adminService.Login(password);

        string message = result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.Failure(WrongPassword) => "Wrong password",
            LoginResult.Failure(NotFound) => "Admin not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}