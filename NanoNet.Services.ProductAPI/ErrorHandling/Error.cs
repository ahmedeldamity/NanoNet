namespace NanoNet.Services.ProductAPI.ErrorHandling;
public class Error(int statusCode, string? title = null)
{
    public static readonly Error? None = new(200, string.Empty);

    public int StatusCode { get; set; } = statusCode;
    public string Title { get; set; } = title ?? GetDefaultMessageForStatusCode(statusCode);

    private static string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request, you have made!",
            401 => "Authorized, you are not!",
            404 => "Resource was not found!",
            500 => "Server Error",
            _ => "Invalid request"
        };
    }
}