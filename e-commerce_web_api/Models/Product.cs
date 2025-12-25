using System.Text.Json.Serialization;

namespace e_commerce_web_api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        
        [JsonIgnore]
        public Category? Category { get; set; }

    }
}
