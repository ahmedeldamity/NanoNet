using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Interfaces.IService;
public interface ICouponService
{
    Task<ResponseViewModel?> GetAllCouponsAsync();
    Task<ResponseViewModel?> GetCouponByIdAsync(int couponId);
    Task<ResponseViewModel?> GetCouponByCodeAsync(string couponCode);
    Task<ResponseViewModel?> CreateCouponAsync(CouponViewModel couponDto);
    Task<ResponseViewModel?> UpdateCouponAsync(CouponViewModel couponDto);
    Task<ResponseViewModel?> DeleteCouponAsync(int id);
}