namespace SaludVirtualAPI.DTOs
{
    public class CargosDto
    {
        public string Id { get; set; }
        public string CodigoCargo { get; set; }
        public string NombreCargo { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}