using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wholesale.Models;
using Wholesale.Server.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Wholesale.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegionHeaderController : ControllerBase
    {
        private readonly IRepository<RegionHeader, int> _repository;

        public RegionHeaderController(IRepository<RegionHeader, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RegionHeader>> Get()
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
        public ActionResult<RegionHeader> Get(int id)
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
        public ActionResult<RegionHeader> Post([FromBody] RegionHeader entity)
        {
            try
            {
                if (entity.Details != null)
                {
                    entity.Details = entity.Details
                        .Where(d => !string.IsNullOrEmpty(d.NameRoute))
                        .ToList();
                }

                entity = _repository.Add(entity);
                return CreatedAtAction(nameof(Get), new { id = entity.RegionId }, entity);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }




        [HttpPut("{id}")]
        public ActionResult<RegionHeader> Put(int id, [FromBody] RegionHeader entity)
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
    }
}
