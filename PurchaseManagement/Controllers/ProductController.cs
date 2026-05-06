using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseManagement.Data;
using PurchaseManagement.DTO;
using PurchaseManagement.Models;

namespace PurchaseManagement.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var prod = await _context.Tb_Products.ToListAsync();

            if (!prod.Any())
            {
                return NoContent();    
            }

            return Ok(prod);
            
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductPostDTO dto)
        {
            
            if(dto == null)
            {
                return BadRequest();
            }

            var prod = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Quantity = dto.Quantity
            };
            
            await _context.Tb_Products.AddAsync(prod);

            await _context.SaveChangesAsync();

            return Ok(prod);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<Product>> PatchProduct(Guid id, ProductPatchDTO dto)
        {
            var exist = await _context.Tb_Products.FindAsync(id);

            if(exist == null)
            {
                return NotFound();
            }


            if(dto.Name != null && string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Invalid name");
            } 

            if(dto.Quantity != null && dto.Quantity <= 0)
            {
                return BadRequest("Invalid Quantity");
            }

            if(dto.Price != null && dto.Price <= 0)
            {
                return BadRequest("Invalid Quantity");
            }

            if(dto.Name != null)
            {
                exist.Name = dto.Name;
            }
            
            if(dto.Quantity != null)
            {
                exist.Quantity = dto.Quantity.Value;
            }
            
            if(dto.Price != null)
            {
                exist.Price = dto.Price.Value;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var prod = await _context.Tb_Products.FindAsync(id);

            if(prod == null)
            {
                return NotFound();
            }

            _context.Tb_Products.Remove(prod);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
