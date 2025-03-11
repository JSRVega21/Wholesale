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
    public class VisitDetailController : ControllerBase
    {
        private readonly IRepository<VisitDetail, int> _repository;

        public VisitDetailController(IRepository<VisitDetail, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VisitDetail>> Get()
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
        public ActionResult<VisitDetail> Get(int id)
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
        public ActionResult<VisitDetail> Post([FromBody] VisitDetail entity)
        {
            try
            {
                entity = _repository.Add(entity);
                return CreatedAtAction(nameof(Get), new { id = entity.VisitDetailId }, entity);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<VisitDetail> Put(int id, [FromBody] VisitDetail entity)
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
