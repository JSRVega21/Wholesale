using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Wholesale.Models;
using Wholesale.server.Repository;
using Sap.Data.Hana;

namespace Wholesale.server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientRepository _clientRepository;

        public ClientController()
        {
            _clientRepository = new ClientRepository();
        }

        [HttpGet("GetClients")]
        public ActionResult<ClientHeaderCLS> GetClients([FromQuery] int? slpCode, [FromQuery] string? uCodigoPOS)
        {
            if (!slpCode.HasValue && string.IsNullOrEmpty(uCodigoPOS))
            {
                return BadRequest("Debe enviar SlpCode o U_CodigoPOS.");
            }

            var result = _clientRepository.GetClients(slpCode, uCodigoPOS);

            if (result == null || result.Clients.Count == 0)
            {   
                return NotFound("No se encontraron clientes.");
            }

            return Ok(result);
        }

        [HttpGet("GetUsersSap")]
        public ActionResult<List<UserSapCLS>> GetUsersSap()
        {
            var result = _clientRepository.GetUsersSap();

            if (result == null || result.Count == 0)
            {
                return NotFound("No se encontraron vendedores.");
            }

            return Ok(result);
        }
    }
}
