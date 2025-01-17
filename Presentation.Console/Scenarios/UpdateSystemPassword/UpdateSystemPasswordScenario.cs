using Application.Contracts.Admins;
using Spectre.Console;

namespace Presentation.Console.Scenarios.UpdateSystemPassword;

public class UpdateSystemPasswordScenario : IScenario
{
    private readonly IAdminService _adminService;

    public UpdateSystemPasswordScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Update System Password";

    public void Run()
    {
        string newPassword = AnsiConsole.Ask<string>("Enter new system password");

        _adminService.UpdateSystemPassword(newPassword);

        AnsiConsole.WriteLine("You have updated system password");
        AnsiConsole.Ask<string>("Ok");
    }
}