using PurchaseManagement.Models;
using System.Text.Json.Serialization;

namespace PurchaseManagement.DTO.Purchase.Request
{
    public class PurchasePostDTO
    {

        public Guid ClientId { get; set; }
 
        public Guid ProductId { get; set; }

        public int QuantityPurchase { get; set; }

    }
}
