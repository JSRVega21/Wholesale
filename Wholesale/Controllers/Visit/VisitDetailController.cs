using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wholesale.Models;
using Wholesale.Server.Repository;

namespace Wholesale.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitDetailController : ControllerBase
    {
        private readonly IRepository<VisitDetail, int> _repository;

        public VisitDetailController(IRepository<VisitDetail, int> repository)
        {
            _repository = repository;
        }

        public class VisitDetailUploadDto
        {
            public int VisitHeaderId { get; set; }
            public string? SalespersonCode { get; set; }
            public string? SalespersonName { get; set; }
            public string? Address { get; set; }
            public string? PhoneNumber { get; set; }
            public string? TypeVisit { get; set; }
            public string? Coordinates { get; set; }
            public string? Comment { get; set; }
            public IFormFile? File { get; set; }
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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<VisitDetail>> Post([FromForm] VisitDetailUploadDto dto)
        {
            try
            {
                var entity = new VisitDetail
                {
                    VisitHeaderId = dto.VisitHeaderId,
                    SalespersonCode = dto.SalespersonCode,
                    SalespersonName = dto.SalespersonName,
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                    TypeVisit = dto.TypeVisit,
                    Coordinates = dto.Coordinates,
                    Comment = dto.Comment
                };

                if (dto.File != null)
                {
                    using var ms = new MemoryStream();
                    await dto.File.CopyToAsync(ms);
                    entity.Photo = ms.ToArray();
                }

                entity = _repository.Add(entity);
                return CreatedAtAction(nameof(Get), new { id = entity.VisitDetailId }, entity);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<VisitDetail>> Put(int id, [FromForm] VisitDetailUploadDto dto)
        {
            try
            {
                var existingEntity = _repository.GetByKey(id);
                if (existingEntity == null)
                {
                    return NotFound();
                }

                // Actualiza las propiedades
                existingEntity.VisitHeaderId = dto.VisitHeaderId;
                existingEntity.SalespersonCode = dto.SalespersonCode;
                existingEntity.SalespersonName = dto.SalespersonName;
                existingEntity.Address = dto.Address;
                existingEntity.PhoneNumber = dto.PhoneNumber;
                existingEntity.TypeVisit = dto.TypeVisit;
                existingEntity.Coordinates = dto.Coordinates;
                existingEntity.Comment = dto.Comment;

                if (dto.File != null)
                {
                    using var ms = new MemoryStream();
                    await dto.File.CopyToAsync(ms);
                    existingEntity.Photo = ms.ToArray();
                }

                var updatedEntity = _repository.Update(existingEntity);
                return Ok(updatedEntity);
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
