using NanoNet.Services.ShoppingCartAPI.Dtos;
using NanoNet.Services.ShoppingCartAPI.Interfaces.IService;
using Newtonsoft.Json;

namespace NanoNet.Services.ShoppingCartAPI.Services
{
    public class CouponService(IHttpClientFactory _httpClientFactory) : ICouponService
    {
        public async Task<CouponDto> GetCouponByCode(string couponCode)
        {
            HttpClient client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupon/GetCouponByCode/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (data is not null && data.IsSuccess)
            {
                // Map the data to the ProductDto
                var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(data.Result));
                return coupon;
            }
            return new CouponDto();
        }

    }
}
