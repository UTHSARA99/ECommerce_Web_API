using System.Text.Json.Serialization;

namespace e_commerce_web_api.Models

{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}
