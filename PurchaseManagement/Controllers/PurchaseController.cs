using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseManagement.Data;
using PurchaseManagement.DTO.Purchase.Request;
using PurchaseManagement.DTO.Purchase.Response;
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
        public async Task<ActionResult<PurchaseResponseDTO>> PostPurchase(PurchasePostDTO dto)
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
            // Decrementado a quantidade comprada
            product.Quantity -= dto.QuantityPurchase;

            var purchase = dto.Adapt<Purchase>();
          
             _context.Tb_Purchases.Add(purchase);

            await _context.SaveChangesAsync();

            purchase = await _context.Tb_Purchases
                .Include(p => p.Client)
                .Include(p => p.Product)
                .FirstOrDefaultAsync();

            var response = purchase.Adapt<PurchaseResponseDTO>();

            return Ok(response);

        }
    }
}
