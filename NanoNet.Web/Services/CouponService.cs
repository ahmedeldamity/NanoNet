using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Services;
public class CouponService(IBaseService baseService) : ICouponService
{
    public async Task<ResponseViewModel?> GetAllCouponsAsync()
    {
        return await baseService.SendAsync(new RequestViewModel
        {
            ApiType = SD.ApiType.GET,
            Url = SD.CouponAPIBase + "/api/coupon"
        });
    }

    public async Task<ResponseViewModel?> GetCouponByIdAsync(int couponId)
    {
        return await baseService.SendAsync(new RequestViewModel
        {
            ApiType = SD.ApiType.GET,
            Url = SD.CouponAPIBase + $"/api/coupon/{couponId}"
        });
    }

    public async Task<ResponseViewModel?> GetCouponByCodeAsync(string couponCode)
    {
        return await baseService.SendAsync(new RequestViewModel
        {
            ApiType = SD.ApiType.GET,
            Url = SD.CouponAPIBase + $"/api/coupon/GetCouponByCode/{couponCode}"
        });
    }

    public async Task<ResponseViewModel?> CreateCouponAsync(CouponViewModel couponDto)
    {
        return await baseService.SendAsync(new RequestViewModel
        {
            ApiType = SD.ApiType.POST,
            Data = couponDto,
            Url = SD.CouponAPIBase + "/api/coupon",
        });
    }

    public async Task<ResponseViewModel?> UpdateCouponAsync(CouponViewModel couponDto)
    {
        return await baseService.SendAsync(new RequestViewModel
        {
            ApiType = SD.ApiType.PUT,
            Data = couponDto,
            Url = SD.CouponAPIBase + "/api/coupon"
        });
    }

    public async Task<ResponseViewModel?> DeleteCouponAsync(int id)
    {
        return await baseService.SendAsync(new RequestViewModel
        {
            ApiType = SD.ApiType.DELETE,
            Url = SD.CouponAPIBase + $"/api/coupon/{id}"
        });
    }
}