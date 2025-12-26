using AutoMapper;
using e_commerce_web_api.Services.CouponAPI.Data;
using e_commerce_web_api.Services.CouponAPI.Models;
using e_commerce_web_api.Services.CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web_api.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ResponseDTO _response;
        private IMapper _mapper;

        public CouponAPIController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO GetById(int id)
        {
            try
            {
                Coupon couponObject = _dbContext.Coupons.First(coupon => coupon.CouponId == id);

                if (couponObject == null)
                {
                    _response.Result = $"No coupon found in the id {id}";
                }
                _response.Result = _mapper.Map<CouponDTO>(couponObject);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
