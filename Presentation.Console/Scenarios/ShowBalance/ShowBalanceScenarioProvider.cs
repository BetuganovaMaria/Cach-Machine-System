using Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Scenarios.ShowBalance;

public class ShowBalanceScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentUserService _currentUser;

    public ShowBalanceScenarioProvider(
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
            scenario = new ShowBalanceScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}