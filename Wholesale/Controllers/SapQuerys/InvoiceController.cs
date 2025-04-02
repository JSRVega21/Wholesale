using Microsoft.AspNetCore.Mvc;
using Wholesale.Models;
using Wholesale.server.Repository;

namespace Wholesale.server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceRepository _invoiceRepository;

        public InvoiceController()
        {
            _invoiceRepository = new InvoiceRepository();
        }

        [HttpGet("GetInvoice")]
        public ActionResult<InvoiceSapCLS> GetInvoice([FromQuery] string? NumAtCard)
        {
            if (NumAtCard == null)
            {
                return BadRequest("Debe proporcionar el número de factura.");
            }

            var result = _invoiceRepository.GetInvoice(NumAtCard);

            if (result == null)
            {
                return NotFound("No se encontró la factura.");
            }

            return Ok(result);
        }

    }
}
