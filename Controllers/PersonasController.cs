using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SaludVirtualAPI.Data;
using SaludVirtualAPI.Models;
using MongoDB.Bson;

namespace SaludVirtualAPI.Controllers
{
    [ApiController]
    [Route("api/Personas")]
    public class PersonaController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<PersonaController> _logger;

        public PersonaController(MongoDbContext context, ILogger<PersonaController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPersonas()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las personas...");

                var personas = await _context.Personas
                    .Find(_ => true)
                    .ToListAsync();

                _logger.LogInformation($"Se encontraron {personas.Count} personas");

                var result = personas.Select(p => new
                {
                    Id = p.Id,
                    identificacion = p.identificacion,
                    PrimerNombre = p.PrimerNombre,
                    SegundoNombre = p.SegundoNombre,
                    Apellido = p.Apellido,
                    Sexo = p.Sexo,
                    Edad = p.Edad,
                    Telefono = p.Telefono,
                    Correo = p.Correo,
                    Direccion = p.Direccion,
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener personas");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}