using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SaludVirtualAPI.Data;
using SaludVirtualAPI.DTOs;
using SaludVirtualAPI.Models;

namespace SaludVirtualAPI.Controllers
{
    [ApiController]
    [Route("api/HistorialMedico")]
    public class HistorialMedicoController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<HistorialMedicoController> _logger;

        public HistorialMedicoController(MongoDbContext context, ILogger<HistorialMedicoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetHistorialMedicoCompleto()
        {
            try
            {
                _logger.LogInformation("Obteniendo historial médico con información completa...");

                // Obtener todos los historiales médicos activos
                var historialesMedicos = await _context.HistorialMedico
                    .Find(hm => hm.Activo)
                    .ToListAsync();

                var resultado = new List<HistorialMedicoCompletoDto>();

                foreach (var historial in historialesMedicos)
                {
                    var historialDto = new HistorialMedicoCompletoDto
                    {
                        // Datos del historial médico (sin PacienteId)
                        Id = historial.Id,
                        CodigoHistorialMedico = historial.CodigoHistorialMedico,
                        Diagnostico = historial.Diagnostico,
                        Tratamiento = historial.Tratamiento,
                        Pronostico = historial.Pronostico,
                        Fecha = historial.Fecha,
                        PesoLibras = historial.PesoLibras,
                        Medida = historial.Medida,
                        Activo = historial.Activo
                    };

                    // Buscar información del Paciente
                    if (!string.IsNullOrEmpty(historial.PacienteId))
                    {
                        var paciente = await _context.Pacientes
                            .Find(p => p.Id == historial.PacienteId)
                            .FirstOrDefaultAsync();

                        if (paciente != null)
                        {
                            historialDto.CodigoPaciente = paciente.CodigoPaciente;
                            historialDto.AlergiasPaciente = paciente.Alergias;

                            // Buscar información de la Persona (Paciente)
                            if (!string.IsNullOrEmpty(paciente.PersonaId))
                            {
                                var personaPaciente = await _context.Personas
                                    .Find(p => p.Id == paciente.PersonaId)
                                    .FirstOrDefaultAsync();

                                if (personaPaciente != null)
                                {
                                    historialDto.PrimerNombrePaciente = personaPaciente.PrimerNombre;
                                    historialDto.ApellidoPaciente = personaPaciente.Apellido;
                                    historialDto.EdadPaciente = personaPaciente.Edad;
                                }
                            }
                        }
                    }

                    resultado.Add(historialDto);
                }

                _logger.LogInformation($"Se procesaron {resultado.Count} historiales médicos completos");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales médicos completos: {Message}", ex.Message);
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHistorialMedicoCompletoById(string id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo historial médico con ID: {id}");

                if (!ObjectId.TryParse(id, out _))
                {
                    return BadRequest("ID no válido");
                }

                // Obtener el historial médico específico
                var historial = await _context.HistorialMedico
                    .Find(hm => hm.Id == id && hm.Activo)
                    .FirstOrDefaultAsync();

                if (historial == null)
                {
                    return NotFound($"Historial médico con ID {id} no encontrado");
                }

                var historialDto = new HistorialMedicoCompletoDto
                {
                    // Datos del historial médico (sin PacienteId)
                    Id = historial.Id,
                    CodigoHistorialMedico = historial.CodigoHistorialMedico,
                    Diagnostico = historial.Diagnostico,
                    Tratamiento = historial.Tratamiento,
                    Pronostico = historial.Pronostico,
                    Fecha = historial.Fecha,
                    PesoLibras = historial.PesoLibras,
                    Medida = historial.Medida,
                    Activo = historial.Activo
                };

                // Buscar información del Paciente
                if (!string.IsNullOrEmpty(historial.PacienteId))
                {
                    var paciente = await _context.Pacientes
                        .Find(p => p.Id == historial.PacienteId)
                        .FirstOrDefaultAsync();

                    if (paciente != null)
                    {
                        historialDto.CodigoPaciente = paciente.CodigoPaciente;
                        historialDto.AlergiasPaciente = paciente.Alergias;

                        // Buscar información de la Persona (Paciente)
                        if (!string.IsNullOrEmpty(paciente.PersonaId))
                        {
                            var personaPaciente = await _context.Personas
                                .Find(p => p.Id == paciente.PersonaId)
                                .FirstOrDefaultAsync();

                            if (personaPaciente != null)
                            {
                                historialDto.PrimerNombrePaciente = personaPaciente.PrimerNombre;
                                historialDto.ApellidoPaciente = personaPaciente.Apellido;
                                historialDto.EdadPaciente = personaPaciente.Edad;
                            }
                        }
                    }
                }

                _logger.LogInformation($"Historial médico con ID {id} obtenido exitosamente");
                return Ok(historialDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial médico por ID: {Message}", ex.Message);
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<ActionResult> GetHistorialMedicoByPacienteId(string pacienteId)
        {
            try
            {
                _logger.LogInformation($"Obteniendo historial médico para paciente ID: {pacienteId}");

                if (!ObjectId.TryParse(pacienteId, out _))
                {
                    return BadRequest("ID de paciente no válido");
                }

                // Obtener todos los historiales médicos del paciente
                var historialesMedicos = await _context.HistorialMedico
                    .Find(hm => hm.PacienteId == pacienteId && hm.Activo)
                    .ToListAsync();

                // Buscar información del Paciente (una sola vez)
                var paciente = await _context.Pacientes
                    .Find(p => p.Id == pacienteId)
                    .FirstOrDefaultAsync();

                if (paciente == null)
                {
                    return NotFound($"Paciente con ID {pacienteId} no encontrado");
                }

                // Buscar información de la Persona (Paciente)
                var personaPaciente = await _context.Personas
                    .Find(p => p.Id == paciente.PersonaId)
                    .FirstOrDefaultAsync();

                var resultado = new List<HistorialMedicoCompletoDto>();

                foreach (var historial in historialesMedicos)
                {
                    var historialDto = new HistorialMedicoCompletoDto
                    {
                        // Datos del historial médico (sin PacienteId)
                        Id = historial.Id,
                        CodigoHistorialMedico = historial.CodigoHistorialMedico,
                        Diagnostico = historial.Diagnostico,
                        Tratamiento = historial.Tratamiento,
                        Pronostico = historial.Pronostico,
                        Fecha = historial.Fecha,
                        PesoLibras = historial.PesoLibras,
                        Medida = historial.Medida,
                        Activo = historial.Activo,

                        // Datos del paciente
                        CodigoPaciente = paciente.CodigoPaciente,
                        AlergiasPaciente = paciente.Alergias
                    };

                    // Asignar datos de la Persona
                    if (personaPaciente != null)
                    {
                        historialDto.PrimerNombrePaciente = personaPaciente.PrimerNombre;
                        historialDto.ApellidoPaciente = personaPaciente.Apellido;
                        historialDto.EdadPaciente = personaPaciente.Edad;
                    }

                    resultado.Add(historialDto);
                }

                _logger.LogInformation($"Se procesaron {resultado.Count} historiales médicos para el paciente {pacienteId}");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial médico por paciente: {Message}", ex.Message);
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}