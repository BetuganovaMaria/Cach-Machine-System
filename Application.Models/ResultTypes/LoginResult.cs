namespace Application.Models.ResultTypes;

public abstract record LoginResult
{
    public sealed record Success : LoginResult;

    public sealed record Failure(IError Error) : LoginResult;
}