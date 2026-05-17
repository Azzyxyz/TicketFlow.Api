namespace TicketFlow.Api.Application.Common;

public sealed class OperationResult<T>
{
    private OperationResult(bool success, T? value, string? error)
    {
        Success = success;
        Value = value;
        Error = error;
    }

    public bool Success { get; }

    public T? Value { get; }

    public string? Error { get; }

    public static OperationResult<T> Ok(T value)
    {
        return new OperationResult<T>(true, value, null);
    }

    public static OperationResult<T> Fail(string error)
    {
        return new OperationResult<T>(false, default, error);
    }
}
