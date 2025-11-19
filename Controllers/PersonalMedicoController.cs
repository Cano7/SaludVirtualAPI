using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SaludVirtualAPI.Data;
using SaludVirtualAPI.DTOs;
using SaludVirtualAPI.Models;

namespace SaludVirtualAPI.Controllers
{
    [ApiController]
    [Route("api/PersonalMedico")]
    public class PersonalMedicoController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<PersonalMedicoController> _logger;

        public PersonalMedicoController(MongoDbContext context, ILogger<PersonalMedicoController> logger)
        {
            _context = context;
            _logger = logger;
        }



        [HttpGet]
        public async Task<ActionResult> GetPersonalMedicoCompleto()
        {
            try
            {
                _logger.LogInformation("Obteniendo personal médico con información completa...");

                // Obtener todo el personal médico
                var personalMedico = await _context.PersonalMedico
                    .Find(_ => true)
                    .ToListAsync();

                var resultado = new List<PersonalMedicoCompletoDto>();

                foreach (var medico in personalMedico)
                {
                    var medicoDto = new PersonalMedicoCompletoDto
                    {
                        // Datos del personal médico
                        CodigoMedico = medico.CodigoMedico,
                        Active = medico.Active,

                    };

                    // Buscar información de la Persona (Médico)
                    if (!string.IsNullOrEmpty(medico.PersonaId))
                    {
                        var persona = await _context.Personas
                            .Find(p => p.Id == medico.PersonaId)
                            .FirstOrDefaultAsync();

                        if (persona != null)
                        {
                            medicoDto.PrimerNombre = persona.PrimerNombre;
                            medicoDto.SegundoNombre = persona.SegundoNombre;
                            medicoDto.Apellido = persona.Apellido;
                            medicoDto.Edad = persona.Edad;
                            medicoDto.Correo = persona.Correo;
                            medicoDto.Direccion = persona.Direccion;
                            medicoDto.Identificacion = persona.identificacion;
                            medicoDto.Sexo = persona.Sexo;
                        }
                    }

                    // Buscar información del Cargo
                    if (!string.IsNullOrEmpty(medico.CargoId))
                    {
                        var cargo = await _context.Cargos
                            .Find(c => c.Id == medico.CargoId)
                            .FirstOrDefaultAsync();

                        if (cargo != null)
                        {
                            medicoDto.NombreCargo = cargo.NombreCargo;
                        }
                    }

                    // Buscar información de la Dependencia
                    if (!string.IsNullOrEmpty(medico.DependenciaId))
                    {
                        var dependencia = await _context.Dependencias
                            .Find(d => d.Id == medico.DependenciaId)
                            .FirstOrDefaultAsync();

                        if (dependencia != null)
                        {
                            medicoDto.NombreDependencia = dependencia.NombreDependencia;
                        }
                    }

                    resultado.Add(medicoDto);
                }

                _logger.LogInformation($"Se procesaron {resultado.Count} registros de personal médico completo");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener personal médico completo: {Message}", ex.Message);
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
