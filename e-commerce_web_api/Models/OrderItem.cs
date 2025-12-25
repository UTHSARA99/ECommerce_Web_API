using System.Text.Json.Serialization;

namespace e_commerce_web_api.Models
{
    public class OrderItem
    {
        public int  OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }

        public int Quantity { get; set; }
    }
}
