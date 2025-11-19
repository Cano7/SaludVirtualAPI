namespace SaludVirtualAPI.Controllers
{
    using global::SaludVirtualAPI.Data;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;



    [ApiController]
    [Route("api/Cargos")]
    public class CargosController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<CargosController> _logger;

        public CargosController(MongoDbContext context, ILogger<CargosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Cargo
        [HttpGet]
        public async Task<ActionResult> GetCargos()
        {
            try
            {
                _logger.LogInformation("Obteniendo cargos...");

                var cargos = await _context.Cargos
                    .Find(_ => true)
                    .ToListAsync();

                _logger.LogInformation($"Se encontraron {cargos.Count} cargos");

                var result = cargos.Select(c => new
                {
                    Id = c.Id,
                    CodigoCargo = c.CodigoCargo,
                    NombreCargo = c.NombreCargo,
                    Activo = c.Activo,
                    FechaModificacion = c.FechaModificacion
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cargos: {Message}", ex.Message);
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}