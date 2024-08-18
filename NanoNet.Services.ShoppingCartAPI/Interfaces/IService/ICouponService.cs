using NanoNet.Services.ShoppingCartAPI.Dtos;

namespace NanoNet.Services.ShoppingCartAPI.Interfaces.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
