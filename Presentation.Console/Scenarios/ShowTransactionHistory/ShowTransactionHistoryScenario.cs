using Application.Contracts.Users;
using Application.Models;
using Spectre.Console;

namespace Presentation.Console.Scenarios.ShowTransactionHistory;

public class ShowTransactionHistoryScenario : IScenario
{
    private readonly IUserService _userService;

    public ShowTransactionHistoryScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Show Transaction History";

    public void Run()
    {
        IList<Transaction> operationHistory = _userService.ShowOperationHistory();

        AnsiConsole.WriteLine("Transaction history:");
        foreach (Transaction transaction in operationHistory)
        {
            AnsiConsole.WriteLine(transaction.Type.ToString());
            AnsiConsole.WriteLine($"- User: {transaction.AccountNumber}");
            AnsiConsole.WriteLine($"- CurrentBalance: {transaction.CurrentBalance}");
        }

        AnsiConsole.Ask<string>("Ok");
    }
}