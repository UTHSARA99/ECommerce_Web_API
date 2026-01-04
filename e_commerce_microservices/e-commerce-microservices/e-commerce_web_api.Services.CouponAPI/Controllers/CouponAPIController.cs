using AutoMapper;
using e_commerce_web_api.Services.CouponAPI.Data;
using e_commerce_web_api.Services.CouponAPI.Models;
using e_commerce_web_api.Services.CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web_api.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
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

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon couponObject = _dbContext.Coupons.FirstOrDefault(coupon => coupon.CouponCode.ToLower() == code.ToLower());

                if (couponObject == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"No coupon found with code {code}";
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

        [HttpPost]
        public ResponseDTO CreateCoupon(CouponDTO couponDto)
        {
            try
            {
                Coupon couponObject = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Add(couponObject);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDTO>(couponObject);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDTO UpdateCoupon([FromBody] CouponDTO couponDto)
        {
            try
            {
                Coupon couponObject = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Update(couponObject);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDTO>(couponObject);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{couponId:int}")]
        public ResponseDTO DeleteCoupon(int couponId)
        {
            try
            {
                Coupon couponObject = _dbContext.Coupons.First(coupon => coupon.CouponId == couponId);
                _dbContext.Coupons.Remove(couponObject);
                _dbContext.SaveChanges();
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
