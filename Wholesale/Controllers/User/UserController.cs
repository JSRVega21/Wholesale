using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wholesale.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Wholesale.Server.Repository;
namespace Wholesale.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository<User, int> _controllerRepository;

        public UserController(IUserRepository<User, int> UserRepository)
        {
            _controllerRepository = UserRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
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
        public ActionResult<User> Get(int id)
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
        public async Task<ActionResult<User>> Post([FromBody] User entity)
        {
            try
            {
                entity = await _controllerRepository.AddAsync(entity);
                return CreatedAtAction(nameof(Get), new { id = entity.UserId }, entity);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserCLS>> Put(int id, [FromBody] UserCLS dto)
        {
            try
            {
                var existingUser = await _controllerRepository.GetByKeyAsync(id);

                if (existingUser == null)
                    return NotFound();

                Console.WriteLine($"Password en BD: {existingUser.UserPassword}");
                Console.WriteLine($"Password recibida: {dto.UserPassword}");

                // Actualizar los campos normales
                existingUser.UserName = dto.UserName;
                existingUser.SlpCode = dto.SlpCode;
                existingUser.U_CodigoPOS = dto.U_CodigoPOS;
                existingUser.SlpName = dto.SlpName;
                existingUser.UserEmail = dto.UserEmail;
                existingUser.UserPhone = dto.UserPhone;
                existingUser.UserRoleId = dto.UserRoleId;
                existingUser.UserRole = dto.UserRole;

                // Solo cambiar la contraseña si realmente el usuario mandó una nueva
                if (!string.IsNullOrWhiteSpace(dto.UserPassword))
                {
                    // Verificamos si la nueva contraseña es igual a la actual
                    if (!BCrypt.Net.BCrypt.Verify(dto.UserPassword, existingUser.UserPassword))
                    {
                        existingUser.UserPassword = BCrypt.Net.BCrypt.HashPassword(dto.UserPassword);
                        Console.WriteLine("Contraseña actualizada.");
                    }
                    else
                    {
                        Console.WriteLine("La contraseña enviada es igual a la actual. No se actualiza.");
                    }
                }
                else
                {
                    Console.WriteLine("No se envió contraseña. Se mantiene la actual.");
                }


                existingUser.Updated();

                // Guardar cambios
                await _controllerRepository.UpdateAsync(existingUser);

                // Enviar respuesta sin la contraseña
                var response = new UserCLS
                {
                    UserId = existingUser.UserId,
                    UserName = existingUser.UserName,
                    SlpCode = existingUser.SlpCode,
                    U_CodigoPOS = existingUser.U_CodigoPOS,
                    SlpName = existingUser.SlpName,
                    UserEmail = existingUser.UserEmail,
                    UserPhone = existingUser.UserPhone,
                    UserRoleId = existingUser.UserRoleId,
                    UserRole = existingUser.UserRole
                };

                return Ok(response);
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
