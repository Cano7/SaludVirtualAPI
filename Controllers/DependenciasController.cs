namespace SaludVirtualAPI.Controllers
{
    using global::SaludVirtualAPI.Data;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;



    namespace SaludVirtualAPI.Controllers
    {
        [ApiController]
        [Route("api/Dependencias")]
        public class DependenciaController : ControllerBase
        {
            private readonly MongoDbContext _context;
            private readonly ILogger<DependenciaController> _logger;

            public DependenciaController(MongoDbContext context, ILogger<DependenciaController> logger)
            {
                _context = context;
                _logger = logger;
            }

            // GET: api/Dependencia
            [HttpGet]
            public async Task<ActionResult> GetDependencias()
            {
                try
                {
                    _logger.LogInformation("Obteniendo dependencias...");

                    var dependencias = await _context.Dependencias
                        .Find(_ => true)
                        .ToListAsync();

                    _logger.LogInformation($"Se encontraron {dependencias.Count} dependencias");

                    var result = dependencias.Select(d => new
                    {
                        Id = d.Id,
                        CodigoDependencia = d.CodigoDependencia,
                        NombreDependencia = d.NombreDependencia,
                        Activo = d.Activo,
                        FechaModificacion = d.FechaModificacion
                    }).ToList();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener dependencias: {Message}", ex.Message);
                    return StatusCode(500, $"Error interno: {ex.Message}");
                }
            }
        }
    }
}