namespace Hiquotroca.API.Application.Wrappers;

public class BaseResult
{
    public bool isSuccess { get; set; }
    public List<Error>? Errors { get; set; }

    public static BaseResult Ok()
        => new() { isSuccess = true };

    public static BaseResult Failure()
        => new() { isSuccess = false };

    public static BaseResult Failure(Error error)
        => new() { isSuccess = false, Errors = [error] };

    public static BaseResult Failure(IEnumerable<Error> errors)
        => new() { isSuccess = false, Errors = errors.ToList() };

    public static implicit operator BaseResult(Error error)
        => new() { isSuccess = false, Errors = [error] };

    public static implicit operator BaseResult(List<Error> errors)
        => new() { isSuccess = false, Errors = errors };

    public BaseResult AddError(Error error)
    {
        Errors ??= [];
        Errors.Add(error);
        isSuccess = false;
        return this;
    }
}
public class BaseResult<TData> : BaseResult
{
    public TData? Data { get; set; }

    public static BaseResult<TData> Ok(TData data)
        => new() { isSuccess = true, Data = data };
    public new static BaseResult<TData> Failure()
        => new() { isSuccess = false };

    public new static BaseResult<TData> Failure(Error error)
        => new() { isSuccess = false, Errors = [error] };

    public new static BaseResult<TData> Failure(IEnumerable<Error> errors)
        => new() { isSuccess = false, Errors = errors.ToList() };

    public static implicit operator BaseResult<TData>(TData data)
        => new() { isSuccess = true, Data = data };

    public static implicit operator BaseResult<TData>(Error error)
        => new() { isSuccess = false, Errors = [error] };

    public static implicit operator BaseResult<TData>(List<Error> errors)
        => new() { isSuccess = false, Errors = errors };
}