namespace SaludVirtualAPI.DTOs
{
    public class PacienteCompletoDto
    {

        // Datos de la persona (paciente)
        public string CodigoPaciente { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Alergias { get; set; }
        public bool Activo { get; set; }




        // Datos del Tutor
        public string NombreTutor { get; set; }
        public string IdentificationTutor { get; set; }
        public string OcupacionTutor { get; set; }



    }
}