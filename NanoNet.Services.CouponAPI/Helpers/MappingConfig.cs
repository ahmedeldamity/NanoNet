using AutoMapper;
using NanoNet.Services.CouponAPI.Dtos;
using NanoNet.Services.CouponAPI.Models;

namespace NanoNet.Services.CouponAPI.Helpers
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Coupon, CouponDto>();
        }
    }
}
