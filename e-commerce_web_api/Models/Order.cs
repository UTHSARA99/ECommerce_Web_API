using System.Text.Json.Serialization;

namespace e_commerce_web_api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }

        public ICollection<OrderItem> OrderItems {  get; set; }
    }
}
