namespace Application.Models.ResultTypes;

public abstract record WithdrawResult
{
    public sealed record Success : WithdrawResult;

    public sealed record Failure : WithdrawResult;
}