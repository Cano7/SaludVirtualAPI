namespace SaludVirtualAPI.DTOs
{
    namespace SaludVirtualAPI.DTOs
    {
        public class DependenciaDto
        {
            public string Id { get; set; }
            public string CodigoDependencia { get; set; }
            public string NombreDependencia { get; set; }
            public bool Activo { get; set; }
            public DateTime? FechaModificacion { get; set; }
        }
    }
}