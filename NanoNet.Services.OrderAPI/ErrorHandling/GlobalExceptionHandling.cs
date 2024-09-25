using System.Net;
using System.Text.Json;

#region Explanation
// -- When We Need To Create Middleware With Inconvention Way We Must:
// ---- 1. Class Name End With (Middleware)
// ---- 2. Ask Clr To Bring (RequestDelegate next) To Move The Next Middleware
// ---- 3. Ask Clr To Bring (ILogger<GlobalExceptionHandling> logger) To Log Exception In Console
// ---- 4. Ask Clr To Bring (IHostEnvironment environment) To Check We In Development | Production Environment
// ---- 5. Create Function With Name InvokeAsync And Take (HttpContext httpContext) To Use It When Sending Response
// -- In This Case We Create This Class For Server Error
// ---- So We Need To Send In Respone Body:
// ------ If We In Development Environment Three Parameters (Status Code, Exception Message, Exception Details) To FrontEnd
// ------ If We In Production Environment One Parameter (Status Code) To Client And We Send (Exception Message, Exception Details) To Database | File -> To Backend Developer
#endregion
namespace NanoNet.Services.OrderAPI.ErrorHandling;
public class GlobalExceptionHandling(RequestDelegate next, ILogger<GlobalExceptionHandling> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment() ?
                new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString()) :
                new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await httpContext.Response.WriteAsync(json);
        }
    }

}