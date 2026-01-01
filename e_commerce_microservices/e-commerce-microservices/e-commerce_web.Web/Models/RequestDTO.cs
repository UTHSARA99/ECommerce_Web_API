using static e_commerce_web.Web.Utility.StaticDetails;

namespace e_commerce_web.Web.Models
{
    public class RequestDTO
    {
        public APIType APIType { get; set; } = APIType.GET;
        public string URL { get; set; } = string.Empty;
        public object? Data { get; set; }
        public string AccessToken { get; set; } = string.Empty;
    }
}
