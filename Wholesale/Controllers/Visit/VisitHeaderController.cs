using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wholesale.Models;
using Wholesale.Server.Repository;
using Microsoft.AspNetCore.Authorization;
using Wholesale.server.Repository;

namespace Wholesale.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitHeaderController : ControllerBase
    {
        private readonly IVisitHeaderRepository<VisitHeader, int> _repository;

        public VisitHeaderController(IVisitHeaderRepository<VisitHeader, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VisitHeader>> Get()
        {
            try
            {
                var entities = _repository.GetList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<VisitHeader> Get(int id)
        {
            try
            {
                var entity = _repository.GetByKey(id);
                if (entity == null)
                {
                    return NotFound();
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<VisitHeader> Post([FromBody] VisitHeader entity)
        {
            try
            {
                if (entity.Details != null)
                {
                    entity.Details = entity.Details
                        .Where(d => d.SalespersonCode != null)
                        .ToList();
                }

                entity = _repository.Add(entity);
                return CreatedAtAction(nameof(Get), new { id = entity.VisitHeaderId }, entity);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult<VisitHeader> Put(int id, [FromBody] VisitHeader entity)
        {
            try
            {
                var existingEntity = _repository.GetByKey(id);
                if (existingEntity == null)
                {
                    return NotFound();
                }

                entity = _repository.Update(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var entity = _repository.GetByKey(id);
                if (entity == null)
                {
                    return NotFound();
                }

                _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<VisitHeader>> Search([FromQuery] int? slpcode, [FromQuery] int? codigopos)
        {
            try
            {
                var result = _repository.GetBySalespersonOrPos(slpcode, codigopos);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
