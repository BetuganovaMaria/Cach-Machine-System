using Application.Contracts.Admins;
using Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Scenarios.Login.LoginUser;

public class LoginUserScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentUserService _currentUser;
    private readonly ICurrentAdminService _currentAdmin;

    public LoginUserScenarioProvider(
        IUserService service,
        ICurrentUserService currentUser,
        ICurrentAdminService currentAdmin)
    {
        _service = service;
        _currentUser = currentUser;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is not null || _currentAdmin.Admin is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginUserScenario(_service);
        return true;
    }
}