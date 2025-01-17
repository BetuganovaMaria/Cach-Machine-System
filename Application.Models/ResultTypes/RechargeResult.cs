namespace Application.Models.ResultTypes;

public abstract record RechargeResult
{
    public sealed record Success : RechargeResult;

    public sealed record Failure : RechargeResult;
}