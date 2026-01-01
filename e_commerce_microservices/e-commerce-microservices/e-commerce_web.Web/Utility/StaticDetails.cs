namespace e_commerce_web.Web.Utility
{
    public class StaticDetails
    {
        public static string CouponAPIBase { get; set; } = "https://localhost:7001/api/coupons/";

        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
