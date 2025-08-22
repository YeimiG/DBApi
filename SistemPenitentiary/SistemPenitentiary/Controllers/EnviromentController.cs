using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemPenitentiary.DTOs;
using SistemPenitentiary.Models;

namespace SistemPenitentiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviromentController : ControllerBase
    {
        private readonly UeUsocotContext _dbContext;

        public EnviromentController(UeUsocotContext dbContext)
        {
            _dbContext = dbContext;
        }
        // POST: api/Environments
        // Crea un nuevo entorno en la base de datos.
        [HttpPost]
        public async Task<IActionResult> CreateEnvironment([FromBody] EnvironmentCreateUpdateDto environmentDto)
        {
            if (environmentDto == null)
            {
                return BadRequest("Datos del entorno incompletos.");
            }

            var newEnvironment = new SistemPenitentiary.Models.Environment
            {
                EnvironmentId = Guid.NewGuid(),
                EnvironmentName = environmentDto.EnvironmentName ?? string.Empty,
                UnrealLevelPath = environmentDto.UnrealLevelPath ?? string.Empty,
                ConfigurationSettings = environmentDto.ConfigurationSettings ?? "{}",
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = null, // Se establece en null al crear
                ResponsibleUserId = environmentDto.ResponsibleUserId
            };

            _dbContext.Environments.Add(newEnvironment);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnvironmentById), new { id = newEnvironment.EnvironmentId }, newEnvironment);
        }

        // GET: api/Environments/{id}
        // Obtiene un entorno específico por su ID.
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEnvironmentById(Guid id)
        {
            var environment = await _dbContext.Environments.FirstOrDefaultAsync(e => e.EnvironmentId == id);

            if (environment == null)
            {
                return NotFound("Entorno no encontrado.");
            }

            return Ok(environment);
        }

        // GET: api/Environments
        // Obtiene todos los entornos.
        [HttpGet]
        public async Task<IActionResult> GetAllEnvironments()
        {
            var environments = await _dbContext.Environments.ToListAsync();
            return Ok(environments);
        }

        // PUT: api/Environments/{id}
        // Actualiza un entorno existente.
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEnvironment(Guid id, [FromBody] EnvironmentCreateUpdateDto environmentDto)
        {
            if (environmentDto == null)
            {
                return BadRequest("Datos de actualización incompletos.");
            }

            var environmentToUpdate = await _dbContext.Environments.FirstOrDefaultAsync(e => e.EnvironmentId == id);

            if (environmentToUpdate == null)
            {
                return NotFound("Entorno no encontrado.");
            }

            // Actualiza solo los campos necesarios.
            environmentToUpdate.EnvironmentName = environmentDto.EnvironmentName ?? environmentToUpdate.EnvironmentName;
            environmentToUpdate.UnrealLevelPath = environmentDto.UnrealLevelPath ?? environmentToUpdate.UnrealLevelPath;
            environmentToUpdate.ConfigurationSettings = environmentDto.ConfigurationSettings ?? environmentToUpdate.ConfigurationSettings;
            environmentToUpdate.LastModifiedAt = DateTime.UtcNow; // Actualiza la marca de tiempo
            environmentToUpdate.ResponsibleUserId = environmentDto.ResponsibleUserId;

            await _dbContext.SaveChangesAsync();

            return NoContent(); // Respuesta 204 (sin contenido), común para actualizaciones exitosas.
        }

        // DELETE: api/Environments/{id}
        // Elimina un entorno por su ID.
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEnvironment(Guid id)
        {
            var environmentToDelete = await _dbContext.Environments.FirstOrDefaultAsync(e => e.EnvironmentId == id);

            if (environmentToDelete == null)
            {
                return NotFound("Entorno no encontrado.");
            }

            _dbContext.Environments.Remove(environmentToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
