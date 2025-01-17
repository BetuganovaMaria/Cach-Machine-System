using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Scenarios.Login.LoginAdmin;
using Presentation.Console.Scenarios.Login.LoginUser;
using Presentation.Console.Scenarios.Logout.LogoutAdmin;
using Presentation.Console.Scenarios.Logout.LogoutUser;
using Presentation.Console.Scenarios.RechargeAccount;
using Presentation.Console.Scenarios.RegisterUser;
using Presentation.Console.Scenarios.ShowBalance;
using Presentation.Console.Scenarios.UpdateSystemPassword;
using Presentation.Console.Scenarios.WithdrawMoney;

namespace Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, LoginUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, RechargeAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, RegisterUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UpdateSystemPasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawMoneyScenarioProvider>();

        return collection;
    }
}