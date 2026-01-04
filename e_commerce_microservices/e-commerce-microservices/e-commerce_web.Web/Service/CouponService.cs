using e_commerce_web.Web.Models;
using e_commerce_web.Web.Service.IService;
using e_commerce_web.Web.Utility;

namespace e_commerce_web.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService) 
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = StaticDetails.APIType.POST,
                Data = couponDto,
                URL = StaticDetails.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDTO> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = StaticDetails.APIType.DELETE,
                URL = StaticDetails.CouponAPIBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDTO> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = StaticDetails.APIType.GET,
                URL = StaticDetails.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDTO> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = StaticDetails.APIType.GET,
                URL = StaticDetails.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDTO> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = StaticDetails.APIType.GET,
                URL = StaticDetails.CouponAPIBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDTO> UpdateCouponAsync(CouponDTO couponDto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                APIType = StaticDetails.APIType.PUT,
                Data = couponDto,
                URL = StaticDetails.CouponAPIBase + "/api/coupon"
            });
        }
    }
}
