namespace Core.Utilities.Results;

public class Result:IResult
{
    public Result(bool succes)
    {
        Success = succes;
    }
    public Result(bool succes,string message):this(succes)
    {
        Message = message;
    }
    public bool Success { get; }
    public string Message { get; }
}
