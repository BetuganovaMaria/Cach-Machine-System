using Application.Contracts.Admins;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Logout.LogoutAdmin;

public class LogoutAdminScenario : IScenario
{
    private readonly IAdminService _adminService;

    public LogoutAdminScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Logout Admin";

    public void Run()
    {
        _adminService.Logout();

        AnsiConsole.WriteLine("You have logged out");
        AnsiConsole.Ask<string>("Ok");
    }
}