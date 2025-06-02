namespace api.Helpers
{
  public class Result<T>
  {
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }

    public static Result<T> Success(T data, string? message = null)
    {
      return new Result<T> { IsSuccess = true, Data = data, ErrorMessage = message };
    }

    public static Result<T> Error(string message)
    {
      return new Result<T> { IsSuccess = false, ErrorMessage = message };
    }
  }
}
