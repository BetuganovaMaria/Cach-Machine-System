using Application.Contracts.Admins;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Scenarios.Logout.LogoutAdmin;

public class LogoutAdminScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminService _currentAdmin;

    public LogoutAdminScenarioProvider(
        IAdminService service,
        ICurrentAdminService currentAdmin)
    {
        _service = service;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.Admin is not null)
        {
            scenario = new LogoutAdminScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}