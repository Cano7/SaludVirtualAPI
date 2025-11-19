using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SaludVirtualAPI.Data;
using SaludVirtualAPI.Models;

namespace SaludVirtualAPI.Controllers
{
    [ApiController]
    [Route("api/Tutores")]
    public class TutorController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<TutorController> _logger;

        public TutorController(MongoDbContext context, ILogger<TutorController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Tutor
        [HttpGet]
        public async Task<ActionResult> GetTutores()
        {
            try
            {
                _logger.LogInformation("Obteniendo tutores...");

                var tutores = await _context.Tutores
                    .Find(_ => true)
                    .ToListAsync();

                _logger.LogInformation($"Se encontraron {tutores.Count} tutores");

                var result = tutores.Select(t => new
                {
                    Id = t.Id,
                    CodigoTutor = t.CodigoTutor,
                    Ocupacion = t.Ocupacion,
                    Active = t.Active,
                    PersonaId = t.PersonaId,
                    FechaModificacion = t.FechaModificacion
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tutores: {Message}", ex.Message);
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}