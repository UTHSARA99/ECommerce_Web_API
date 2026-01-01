namespace e_commerce_web.Web.Models
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Response Successful.";
    }
}
