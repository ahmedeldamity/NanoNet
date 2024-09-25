namespace NanoNet.Services.EmailAPI.ErrorHandling;
public class Result(bool isSuccess, string error)
{
    public bool IsSuccess { get; } = isSuccess;
    public string Error { get; } = error;

    public static Result Success() => new(true, "");
    public static Result Success(string successMessage) => new(true, successMessage);
    public static Result Failure(string failureMessage) => new(false, failureMessage);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, "");
    public static Result<TValue> Failure<TValue>(string error) => new(default, false, error);
}

public class Result<TValue>(TValue? value, bool isSuccess, string error) : Result(isSuccess, error)
{
    public TValue Value => IsSuccess ? value! : throw new InvalidOperationException("Failure results cannot have value");
}