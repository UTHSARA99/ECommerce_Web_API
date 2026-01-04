using System.Net;
using System.Text;
using e_commerce_web.Web.Models;
using e_commerce_web.Web.Service.IService;
using Newtonsoft.Json;
using static e_commerce_web.Web.Utility.StaticDetails;

namespace e_commerce_web.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTO> SendAsync(RequestDTO requestDTO)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("E-CommerceAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                // token

                Console.WriteLine(requestDTO.URL.ToString());

                message.RequestUri = new Uri(requestDTO.URL);

                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDTO.APIType)
                {
                    case APIType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case APIType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case APIType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDTO { IsSuccess = false, Message = "Not Found." };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDTO { IsSuccess = false, Message = "Access Denied." };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDTO { IsSuccess = false, Message = "Unauthorized." };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDTO { IsSuccess = false, Message = "Internal Server Error." };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return responseDTO!;
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message.ToString()
                };
            }
        }
    }
}
