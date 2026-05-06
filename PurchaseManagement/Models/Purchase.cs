using System.Text.Json.Serialization;

namespace PurchaseManagement.Models
{
    public class Purchase
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
        [JsonIgnore]
        public Client Client { get; set; }
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public int QuantityPurchase { get; set; }
        public DateTimeOffset PurchaseDate { get; set; } = DateTimeOffset.UtcNow;
    }
}
