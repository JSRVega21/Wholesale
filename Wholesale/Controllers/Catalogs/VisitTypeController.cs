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
    public class VisitTypeController : ControllerBase
    {
        private readonly IRepository<VisitType, int> _controllerRepository;

        public VisitTypeController(IRepository<VisitType, int> VisitTypeRepository)
        {
            _controllerRepository = VisitTypeRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VisitType>> Get()
        {
            try
            {
                var entities = _controllerRepository.GetList();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<VisitType> Get(int id)
        {
            try
            {
                var entity = _controllerRepository.GetByKey(id);
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
        public ActionResult<VisitType> Post([FromBody] VisitType entity)
        {
            try
            {
                entity = _controllerRepository.Add(entity);
                return CreatedAtAction(nameof(Get), new { id = entity.VisitTypeId }, entity);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<VisitType> Put(int id, [FromBody] VisitType entity)
        {
            try
            {
                var existingEntity = _controllerRepository.GetByKey(id);
                if (existingEntity == null)
                {
                    return NotFound();
                }

                entity = _controllerRepository.Update(entity);
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
                var entity = _controllerRepository.GetByKey(id);
                if (entity == null)
                {
                    return NotFound();
                }

                _controllerRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
