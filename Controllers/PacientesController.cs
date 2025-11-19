using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SaludVirtualAPI.Data;
using SaludVirtualAPI.DTOs;
using SaludVirtualAPI.Models;

namespace SaludVirtualAPI.Controllers
{
    [ApiController]
    [Route("api/Pacientes")]
    public class PacientesController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<PacientesController> _logger;

        public PacientesController(MongoDbContext context, ILogger<PacientesController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult> GetPacientesCompletos()
        {
            try
            {
                _logger.LogInformation("Obteniendo pacientes con información completa...");

                // Obtener todos los pacientes
                var pacientes = await _context.Pacientes
                    .Find(_ => true)
                    .ToListAsync();

                var resultado = new List<PacienteCompletoDto>();

                foreach (var paciente in pacientes)
                {
                    var pacienteDto = new PacienteCompletoDto
                    {
                        // Datos del paciente
                        CodigoPaciente = paciente.CodigoPaciente,
                        FechaNacimiento = paciente.FechaNacimiento,
                        Alergias = paciente.Alergias,
                        Activo = paciente.Activo,
                    };

                    // Buscar información de la Persona (Paciente)
                    if (!string.IsNullOrEmpty(paciente.PersonaId))
                    {
                        var persona = await _context.Personas
                            .Find(p => p.Id == paciente.PersonaId)
                            .FirstOrDefaultAsync();

                        if (persona != null)
                        {
                            pacienteDto.PrimerNombre = persona.PrimerNombre;
                            pacienteDto.SegundoNombre = persona.SegundoNombre;
                            pacienteDto.Apellido = persona.Apellido;
                            pacienteDto.Direccion = persona.Direccion;
                            pacienteDto.Sexo = persona.Sexo;
                            pacienteDto.Edad = persona.Edad;

                        }
                    }

                    // Buscar información del Tutor
                    if (!string.IsNullOrEmpty(paciente.TutorId))
                    {
                        var tutor = await _context.Tutores
                            .Find(t => t.Id == paciente.TutorId)
                            .FirstOrDefaultAsync();

                        if (tutor != null)
                        {
                            pacienteDto.OcupacionTutor = tutor.Ocupacion;

                            // Buscar información de la Persona (Tutor)
                            var personaTutor = await _context.Personas
                                .Find(p => p.Id == tutor.PersonaId)
                                .FirstOrDefaultAsync();

                            if (personaTutor != null)
                            {
                                pacienteDto.NombreTutor = $"{personaTutor.PrimerNombre} {personaTutor.SegundoNombre} {personaTutor.Apellido}";
                                pacienteDto.IdentificationTutor = personaTutor.identificacion;
                            }
                        }
                    }

                    resultado.Add(pacienteDto);
                }

                _logger.LogInformation($"Se procesaron {resultado.Count} pacientes completos");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pacientes completos: {Message}", ex.Message);
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}