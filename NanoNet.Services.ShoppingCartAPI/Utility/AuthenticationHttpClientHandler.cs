using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace NanoNet.Services.ShoppingCartAPI.Utility;
public class AuthenticationHttpClientHandler(IHttpContextAccessor _accessor): DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _accessor.HttpContext!.GetTokenAsync("access_token");
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken);
    }
}
