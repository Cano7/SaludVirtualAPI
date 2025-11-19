namespace SaludVirtualAPI.DTOs
{
    public class PersonalMedicoCompletoDto
    {

        // Datos de la Persona (Médico)
        public string Identificacion { get; set; }
        public string CodigoMedico { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Sexo { get; set; }
        public bool Active { get; set; }
        public string NombreCargo { get; set; }
        public string NombreDependencia { get; set; }
    }
}