namespace PurchaseManagement.DTO.Product.Request
{
    public class ProductPatchDTO
    {
        public string? Name { get; set; }
        public Double? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
