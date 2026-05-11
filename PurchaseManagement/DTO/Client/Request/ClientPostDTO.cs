using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PurchaseManagement.DTO.Client.Request
{
 
    public class ClientPostDTO 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
    }
}
