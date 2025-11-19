using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SaludVirtualAPI.Data;
using SaludVirtualAPI.DTOs;
using SaludVirtualAPI.Models;

namespace SaludVirtualAPI.Controllers
{
    [ApiController]
    [Route("api/Citas")]
    public class CitaController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<CitaController> _logger;

        public CitaController(MongoDbContext context, ILogger<CitaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetCitasCompletas()
        {
            try
            {
                _logger.LogInformation("Obteniendo citas con información completa...");

                // Obtener todas las citas
                var citas = await _context.Citas
                    .Find(_ => true)
                    .ToListAsync();

                var resultado = new List<CitaCompletaDto>();

                foreach (var cita in citas)
                {
                    var citaDto = new CitaCompletaDto
                    {
                        // Datos de la cita
                        CodigoCita = cita.CodigoCita,
                        Razon = cita.Razon,
                        Estado = cita.Estado,
                        Activo = cita.Activo,

                    };

                    // Buscar información del Paciente
                    if (!string.IsNullOrEmpty(cita.PacienteId))
                    {
                        var paciente = await _context.Pacientes
                            .Find(p => p.Id == cita.PacienteId)
                            .FirstOrDefaultAsync();

                        if (paciente != null)
                        {
                            citaDto.AlergiasPaciente = paciente.Alergias;

                            // Buscar información de la Persona (Paciente)
                            if (!string.IsNullOrEmpty(paciente.PersonaId))
                            {
                                var personaPaciente = await _context.Personas
                                    .Find(p => p.Id == paciente.PersonaId)
                                    .FirstOrDefaultAsync();

                                if (personaPaciente != null)
                                {
                                    citaDto.PrimerNombrePaciente = personaPaciente.PrimerNombre;
                                    citaDto.ApellidoPaciente = personaPaciente.Apellido;
                                    citaDto.EdadPaciente = personaPaciente.Edad;
                                }
                            }
                        }
                    }

                    // Buscar información del Personal Médico
                    if (!string.IsNullOrEmpty(cita.PersonalMedicoId))
                    {
                        var personalMedico = await _context.PersonalMedico
                            .Find(pm => pm.Id == cita.PersonalMedicoId)
                            .FirstOrDefaultAsync();

                        if (personalMedico != null)
                        {

                            // Buscar información de la Persona (Médico)
                            if (!string.IsNullOrEmpty(personalMedico.PersonaId))
                            {
                                var personaMedico = await _context.Personas
                                    .Find(p => p.Id == personalMedico.PersonaId)
                                    .FirstOrDefaultAsync();

                                if (personaMedico != null)
                                {
                                    citaDto.PrimerNombreMedico = personaMedico.PrimerNombre;
                                    citaDto.ApellidoMedico = personaMedico.Apellido;
                                }
                            }

                            // Buscar información del Cargo
                            if (!string.IsNullOrEmpty(personalMedico.CargoId))
                            {
                                var cargo = await _context.Cargos
                                    .Find(c => c.Id == personalMedico.CargoId)
                                    .FirstOrDefaultAsync();

                                if (cargo != null)
                                {
                                    citaDto.NombreCargo = cargo.NombreCargo;
                                }
                            }

                            // Buscar información de la Dependencia
                            if (!string.IsNullOrEmpty(personalMedico.DependenciaId))
                            {
                                var dependencia = await _context.Dependencias
                                    .Find(d => d.Id == personalMedico.DependenciaId)
                                    .FirstOrDefaultAsync();

                                if (dependencia != null)
                                {
                                }
                            }
                        }
                    }

                    resultado.Add(citaDto);
                }

                _logger.LogInformation($"Se procesaron {resultado.Count} citas completas");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener citas completas: {Message}", ex.Message);
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

    }
}