using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PurchaseManagement.Data;
using PurchaseManagement.DTO.Client.Request;
using PurchaseManagement.DTO.Client.Response;
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
        public async Task<ActionResult<IEnumerable<ClientGetDTOResponse>>> Get()
        {
            var client = await _context.Tb_Client.ToListAsync();

            // Any() - Verifica se existe pelo menos 1 item na lista
            // !Any() - Verifica se não existe pelo menos 1 item na lista

            if (!client.Any())
            {
                return NoContent();
            }

           var response = client.Adapt<List<ClientGetDTOResponse>>();
           
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ClientPostDTO>> Post(ClientPostDTO dto)
        {
            if(dto.Cpf.Length != 11)
            {
                return BadRequest("CPF Invalid");
            }

            var existCpf = await _context.Tb_Client.AnyAsync(c => c.Cpf == dto.Cpf);

            if (existCpf)
            {
                return BadRequest("Cpf already registered");
            }

            // Transformando DTO em Entity
            var client = dto.Adapt<Client>();

            var cli = await _context.Tb_Client.AddAsync(client);

            await _context.SaveChangesAsync();

            // Transformando Entity em DTO
            var response = client.Adapt<ClientGetDTOResponse>();

            return Ok(response);
        }

        [HttpPatch]
        public async Task<ActionResult<ClientPatchDTO>> Patch(ClientPatchDTO dto, Guid id)
        {
            var exist = await _context.Tb_Client.FindAsync(id);

            if (exist == null)
            {
                return NotFound();
            }

            if (dto.Name != null && string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest("invalid Name");
            }
            if (dto.Email != null && string.IsNullOrEmpty(dto.Email))
            {
                return BadRequest("Invalid Email");
            }

            if (dto.Name != null)
            {
                exist.Name = dto.Name;
            }

            if (dto.Email != null)
            {
                exist.Email = dto.Email;
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
