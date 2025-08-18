using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemPenitentiary.Models;


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
        [HttpGet]
        [Route("Obtener/{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return NotFound(new { mensaje = "Usuario no encontrado" });

            return StatusCode(StatusCodes.Status200OK, user);
        }

        // POST: api/User/Nuevo
        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            user.UserId = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

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
