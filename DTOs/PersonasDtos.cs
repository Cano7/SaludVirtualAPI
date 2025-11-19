namespace SaludVirtualAPI.DTOs
{
    public class PersonasDto
    {
        public string Id { get; set; }
        public string identificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

        public string NombreCompleto => $"{PrimerNombre} {SegundoNombre} {Apellido}";
    }
}