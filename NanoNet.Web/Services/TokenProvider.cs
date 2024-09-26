using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;

namespace NanoNet.Web.Services;
public class TokenProvider(IHttpContextAccessor contextAccessor) : ITokenProvider
{
    public void ClearToken()
    {
        contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenName);
    }

    public string? GetToken()
    {
        string? token = null;
        var hasToken = contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenName, out token);
        return hasToken is true ? token : null;
    }

    public void SetToken(string token)
    {
        contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenName, token);
    }
}