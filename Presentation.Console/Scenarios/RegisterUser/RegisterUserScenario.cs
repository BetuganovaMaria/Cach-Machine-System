using Application.Contracts.Admins;
using Spectre.Console;

namespace Presentation.Console.Scenarios.RegisterUser;

public class RegisterUserScenario : IScenario
{
    private readonly IAdminService _adminService;

    public RegisterUserScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Register User";

    public void Run()
    {
        long accountNumber = AnsiConsole.Ask<long>("Enter your account number");
        string password = AnsiConsole.Ask<string>("Enter your password");

        _adminService.RegisterNewUser(accountNumber, password);

        AnsiConsole.WriteLine("You have registered");
        AnsiConsole.Ask<string>("Ok");
    }
}