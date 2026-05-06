using System.Text.Json.Serialization;

namespace PurchaseManagement.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public List<Purchase> Purchases { get; set; }
    }
}
