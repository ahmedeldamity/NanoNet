using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using static NanoNet.Web.Utility.SD;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace NanoNet.Web.Services;
public class BaseService(IHttpClientFactory _httpClientFactory, ITokenProvider _tokenProvider) : IBaseService
{
    public async Task<ResponseViewModel?> SendAsync(RequestViewModel requestDto, bool withBearer = true)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient("NanoNetAPI");

            var token = _tokenProvider.GetToken();

            using var message = CreateHttpRequestMessage(requestDto, token);

            var apiResponse = await client.SendAsync(message);

            var apiContent = await apiResponse.Content.ReadAsStringAsync();

            var x = JsonConvert.DeserializeObject<ResponseViewModel>(apiContent);

            var messageToResend = "";

            if (x is not null)
                messageToResend = x.Error;

            return await HandleResponseAsync(apiResponse, messageToResend);
        }
        catch (Exception ex) 
        {
            var response = new ResponseViewModel
            {
                Error = ex.Message,
                IsSuccess = false
            };
            return response;
        }
    }

    private static HttpRequestMessage CreateHttpRequestMessage(RequestViewModel requestDto, string token)
    {
        var message = new HttpRequestMessage
        {
            RequestUri = new Uri(requestDto.Url),
            Method = requestDto.ApiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.DELETE => HttpMethod.Delete,
                ApiType.PUT => HttpMethod.Put,
                _ => HttpMethod.Get
            },
            Headers = { { "Accept", "application/json" }, { "Authorization", $"Bearer {token}" } }
        };

        if (requestDto.Data is not null)
        {
            message.Content = new StringContent(
                JsonConvert.SerializeObject(requestDto.Data),
                Encoding.UTF8,
                "application/json"
            );
        }

        return message;
    }

    private async Task<ResponseViewModel?> HandleResponseAsync(HttpResponseMessage apiResponse, string message)
    {
        return apiResponse.StatusCode switch
        {
            HttpStatusCode.NotFound => CreateErrorResponse(message == "" ? "Not Found" : message),
            HttpStatusCode.Forbidden => CreateErrorResponse(message == "" ? "Access Denied" : message),
            HttpStatusCode.Unauthorized => CreateErrorResponse(message == "" ? "Unauthorized" : message),
            HttpStatusCode.InternalServerError => CreateErrorResponse(message == "" ? "Internal Server Error" : message),
            HttpStatusCode.BadRequest => CreateErrorResponse(message == "" ? "Bad Request You Have Made" : message),
            _ => await CreateResponseFromContentAsync(apiResponse)
        };
    }

    private static ResponseViewModel CreateErrorResponse(string message)
    {
        return new ResponseViewModel
        {
            IsSuccess = false,
            Error = message
        };
    }

    private static async Task<ResponseViewModel?> CreateResponseFromContentAsync(HttpResponseMessage apiResponse)
    {
        var apiContent = await apiResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ResponseViewModel>(apiContent);
    }
}