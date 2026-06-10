using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademiaMusicaAPI.Models;

namespace AcademiaMusicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly AcademiasApiDbContext _context;

        public EstudiantesController(AcademiasApiDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        // 2. GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return NotFound(new { mensaje = "Estudiante no encontrado" });
            return estudiante;
        }

        // 3. POST: api/Estudiantes
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante([FromBody] Estudiante estudiante)
        {
            if (estudiante == null) return BadRequest(new { mensaje = "Datos inválidos" });

            // Forzar el ID a 0 asegura que SQL Server use el IDENTITY(1,1) de la BD sin conflictos
            estudiante.Id = 0;

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.Id }, estudiante);
        }

       // 4. PUT: api/Estudiantes/5 (Modificar un estudiante)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, [FromBody] Estudiante estudiante)
        {
            if (estudiante == null || id != estudiante.Id) 
            {
                return BadRequest(new { mensaje = "El ID no coincide o los datos son inválidos" });
            }

            // 1. Buscamos el estudiante real que está guardado en la base de datos
            var estudianteExistente = await _context.Estudiantes.FindAsync(id);
            if (estudianteExistente == null) 
            {
                return NotFound(new { mensaje = "El estudiante no existe en la base de datos" });
            }

            // 2. Le caemos encima a los datos viejos con los datos nuevos que vienen de la web
            estudianteExistente.Nombre = estudiante.Nombre;
            estudianteExistente.Instrumento = estudiante.Instrumento;
            estudianteExistente.Activo = estudiante.Activo;

            try
            {
                // 3. Guardamos los cambios físicamente en SQL Server
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Estudiantes.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return Ok(new { mensaje = "Estudiante actualizado correctamente", datos = estudianteExistente });
        }

        // 5. DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return NotFound(new { mensaje = "Estudiante no encontrado" });

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Estudiante eliminado correctamente" });
        }
    }
}