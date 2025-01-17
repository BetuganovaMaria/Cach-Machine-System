using Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Scenarios.Logout.LogoutUser;

public class LogoutUserScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentUserService _currentUser;

    public LogoutUserScenarioProvider(
        IUserService service,
        ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is not null)
        {
            scenario = new LogoutUserScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}