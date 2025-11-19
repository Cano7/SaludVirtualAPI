namespace SaludVirtualAPI.DTOs
{
    public class CitaCompletaDto
    {
        private object NombreCompletoMedico;
        //Campos citas
        public string CodigoCita { get; set; }
        public string Razon { get; set; }
        public string Estado { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Activo { get; set; }
        //Campos pacientes
        public string PrimerNombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public int EdadPaciente { get; set; }
        public string AlergiasPaciente { get; set; }
        //campos medicos
        public string PrimerNombreMedico { get; set; }
        public string ApellidoMedico { get; set; }
        public string NombreCargo { get; set; }


    }
}