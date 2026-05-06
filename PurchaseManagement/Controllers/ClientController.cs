using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PurchaseManagement.Data;
using PurchaseManagement.Models;

namespace PurchaseManagement.Controllers
{
    [Route("Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            var client = await _context.Tb_Client.ToListAsync();

            // Any() - Verifica se existe pelo menos 1 item na lista
            // !Any() - Verifica se não existe pelo menos 1 item na lista

            if (!client.Any())
            {
                return NoContent();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client client)
        {
            var cli = await _context.Tb_Client.AddAsync(client);

            await _context.SaveChangesAsync();

            return Ok(client);
        }

        [HttpPatch]
        public async Task<ActionResult<Client>> Patch(Client client, Guid id)
        {
            var exist = await _context.Tb_Client.FindAsync(id);

            if (exist == null)
            {
                return NotFound();
            }

            if (client.Name != null && string.IsNullOrEmpty(client.Name))
            {
                return BadRequest("invalid Name");
            }
            if (client.Email != null && string.IsNullOrEmpty(client.Email))
            {
                return BadRequest("Invalid Email");
            }
            if (client.Cpf != null && string.IsNullOrEmpty(client.Cpf))
            {
                return BadRequest("Invalid Email");
            }

            if (client.Name != null)
            {
                exist.Name = client.Name;
            }

            if (client.Email != null)
            {
                exist.Email = client.Email;
            }

            if (client.Cpf != null)
            {
                exist.Cpf = client.Cpf;
            }

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> Delete(Guid id)
        {
            var client = await _context.Tb_Client.FindAsync(id);

            if(client == null)
            {
                return NotFound();
            }

             _context.Tb_Client.Remove(client);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
