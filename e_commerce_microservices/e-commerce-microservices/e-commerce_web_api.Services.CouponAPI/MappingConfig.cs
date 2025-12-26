using AutoMapper;
using e_commerce_web_api.Services.CouponAPI.Models;
using e_commerce_web_api.Services.CouponAPI.Models.DTOs;

namespace e_commerce_web_api.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDTO, Coupon>();
                config.CreateMap<Coupon, CouponDTO>();
            });
            return mappingConfig;
        }
    }
}
