namespace Application.Core;

/*
 * Will output this information in Development mode
 * Not in Deployment mode
 */
public class AppError
{
	public AppError(int statusCode, string message, string? details = null)
	{
		StatusCode = statusCode;
		Message = message;
		Details = details!;
	}

	public int StatusCode { get; set; }
	public string Message { get; set; }
	public string Details { get; set; }
}
