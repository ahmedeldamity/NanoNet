using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.ViewModels;
using static NanoNet.Web.Utility.SD;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace NanoNet.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseViewModel?> SendAsync(RequestViewModel requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("NanoNetAPI");

                using var message = CreateHttpRequestMessage(requestDto);

                var apiResponse = await client.SendAsync(message);

                return await HandleResponseAsync(apiResponse);
            }
            catch (Exception ex) 
            {
                var response = new ResponseViewModel
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return response;
            }
        }


        private HttpRequestMessage CreateHttpRequestMessage(RequestViewModel requestDto)
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
                Headers = { { "Accept", "application/json" } }
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

        private async Task<ResponseViewModel?> HandleResponseAsync(HttpResponseMessage apiResponse)
        {
            return apiResponse.StatusCode switch
            {
                HttpStatusCode.NotFound => CreateErrorResponse("Not Found"),
                HttpStatusCode.Forbidden => CreateErrorResponse("Access Denied"),
                HttpStatusCode.Unauthorized => CreateErrorResponse("Unauthorized"),
                HttpStatusCode.InternalServerError => CreateErrorResponse("Internal Server Error"),
                _ => await CreateResponseFromContentAsync(apiResponse)
            };
        }

        private ResponseViewModel CreateErrorResponse(string message)
        {
            return new ResponseViewModel
            {
                IsSuccess = false,
                Message = message
            };
        }

        private async Task<ResponseViewModel?> CreateResponseFromContentAsync(HttpResponseMessage apiResponse)
        {
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseViewModel>(apiContent);
        }
    }
}
