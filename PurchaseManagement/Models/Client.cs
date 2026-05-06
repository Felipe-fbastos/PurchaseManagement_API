using System.Text.Json.Serialization;

namespace PurchaseManagement.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        [JsonIgnore]
        public List<Purchase> Purchases { get; set; } = new();

      
    }
}
