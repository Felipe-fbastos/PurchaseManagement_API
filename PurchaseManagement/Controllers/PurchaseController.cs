using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseManagement.Data;
using PurchaseManagement.DTO;
using PurchaseManagement.Models;

namespace PurchaseManagement.Controllers
{
    [Route("Purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public PurchaseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(PurchasePostDTO dto)
        {
            var client = await _context.Tb_Client.FindAsync(dto.ClientId);
            if(client == null)
            {
                return NotFound("Not Found Client");
            }

            var product = await _context.Tb_Products.FindAsync(dto.ProductId);
            if(product == null)
            {
                return NotFound("Not Found Product");
            }

            if(product.Quantity < dto.QuantityPurchase)
            {
                return BadRequest($"Insufficient stock we have only {product.Quantity} this product");
            }

            product.Quantity -= dto.QuantityPurchase;

            var purchase = new Purchase
            {
                ClientId = dto.ClientId,
                ProductId = dto.ProductId,
                QuantityPurchase = dto.QuantityPurchase,
            };
          
             _context.Tb_Purchases.Add(purchase);

            await _context.SaveChangesAsync();

            return Ok(purchase);

        }
    }
}
