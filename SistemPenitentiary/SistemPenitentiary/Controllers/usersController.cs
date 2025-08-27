using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemPenitentiary.Models;
using SistemPenitentiary.Dptos;

namespace penitentiry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UeUsocotContext _dbContext;

        public UserController(UeUsocotContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/User/Lista
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _dbContext.Users.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, users);
        }

        // GET: api/User/Obtener/{id}
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] SistemPenitentiary.Dptos.LoginRequest request)
        {
           
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.PasswordHash))
            {
                return BadRequest(new { mensaje = "Datos de inicio de sesión incompletos." });
            }

 
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });
            }


            if (!BCrypt.Net.BCrypt.Verify(request.PasswordHash, user.PasswordHash))
            {
                // If the passwords don't match, return an Unauthorized response
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });
            }


            return Ok(new { mensaje = "Inicio de sesión exitoso", userId = user.UserId, username = user.Username });
        }

        // POST: api/User/Nuevo
        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            // Genera un nuevo ID para el usuario y la fecha de creación
            user.UserId = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;

            // Hashea la contraseña antes de asignarla
            // BCrypt.Net genera el salt y el hash en un solo paso
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            // Agrega el usuario a la base de datos
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Devuelve una respuesta con el estado 201 Created
            return StatusCode(StatusCodes.Status201Created, new { mensaje = "Usuario creado", userId = user.UserId });
        }

        // PUT: api/User/Editar
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (existingUser == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.LastLoginAt = user.LastLoginAt ?? existingUser.LastLoginAt;

            _dbContext.Users.Update(existingUser);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario actualizado" });
        }

        // DELETE: api/User/Eliminar/{id}
        [HttpDelete]
        [Route("Eliminar/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario eliminado" });
        }
    }
}
