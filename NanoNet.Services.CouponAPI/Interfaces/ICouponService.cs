using NanoNet.Services.CouponAPI.Dtos;
using NanoNet.Services.CouponAPI.ErrorHandling;

namespace NanoNet.Services.CouponAPI.Interfaces;
public interface ICouponService
{
    Task<Result<IReadOnlyList<CouponDto>>> GetAllCoupons();
    Task<Result<CouponDto>> GetCouponById(int id);
    Task<Result<CouponDto>> GetCouponByCode(string code);
    Task<Result<CouponDto>> AddCoupon(CouponDto couponDto);
    Task<Result<CouponDto>> UpdateCoupon(CouponDto couponDto);
    Task<Result> DeleteCoupon(int id);
}