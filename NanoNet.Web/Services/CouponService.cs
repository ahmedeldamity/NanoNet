using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Services
{
    public class CouponService: ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseViewModel?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseViewModel?> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + $"/api/coupon/{couponId}"
            });
        }

        public async Task<ResponseViewModel?> GetCouponByCodeAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + $"/api/coupon/GetCouponByCode/{couponCode}"
            });
        }

        public async Task<ResponseViewModel?> CreateCouponAsync(CouponViewModel couponDto)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.POST,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon",
            });
        }

        public async Task<ResponseViewModel?> UpdateCouponAsync(CouponViewModel couponDto)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseViewModel?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + $"/api/coupon/{id}"
            });
        }
    }
}
