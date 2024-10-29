namespace Application.Core;

public class Result<T>
{
	public bool IsSuccess { get; set; }
	public required T Value { get; set; }
	public required string Error { get; set; }
	public static Result<T> Success(T value)
	{
		return new() { IsSuccess = true, Value = value, Error = string.Empty };
	}

	public static Result<T> Failure(string error)
	{
		return new() { IsSuccess = false, Error = error, Value = default! };
	}
}
