namespace SaludVirtualAPI.DTOs
{
    public class TutorDto
    {
        public string Id { get; set; }
        public string CodigoTutor { get; set; }
        public string Ocupacion { get; set; }
        public bool Active { get; set; }
        public string PersonaId { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Información adicional (opcional)
        public string NombrePersona { get; set; }
        public string Identification { get; set; }
        public string Telefono { get; set; }
    }
}