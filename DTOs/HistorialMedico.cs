namespace SaludVirtualAPI.DTOs
{
    public class HistorialMedicoCompletoDto
    {
        // Datos del Historial Médico
        public string Id { get; set; }
        public string CodigoHistorialMedico { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Pronostico { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PesoLibras { get; set; }
        public decimal Medida { get; set; }
        public bool Activo { get; set; }

        // Datos del Paciente (sin el ID)
        public string CodigoPaciente { get; set; }
        public string PrimerNombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public int EdadPaciente { get; set; }
        public string AlergiasPaciente { get; set; }
    }
}