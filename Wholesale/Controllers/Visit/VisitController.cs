using Microsoft.AspNetCore.Mvc;
using Wholesale.Server.Repository;
using Wholesale.Models;
using System;
using System.Collections.Generic;

namespace Wholesale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IVisitRepository _filterRepository;

        public VisitController(IVisitRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }

        [HttpGet("filter")]
        public ActionResult<IEnumerable<VisitHeader>> GetFiltered(
            [FromQuery] int? id,
            [FromQuery] string? region,
            [FromQuery] string? routes,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? SlpCode,
            [FromQuery] int? U_CodigoPOS,
            [FromQuery] string? SlpName,
            [FromQuery] string? salespersonCode,
            [FromQuery] string? salespersonName)
        {
            try
            {
                var results = _filterRepository.GetFiltered(id, region, routes, startDate, endDate, SlpCode, U_CodigoPOS, SlpName, salespersonCode, salespersonName);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
