using e_commerce_web.Web.Models;

namespace e_commerce_web.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO> SendAsync<T>(RequestDTO requestDTO);
    }
}
