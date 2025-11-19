namespace SaludVirtualAPI.Models
{ 
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaludVirtualAPI.Models
{
    public class Citas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CodigoCita")]
        public string CodigoCita { get; set; }

        [BsonElement("Razon")]
        public string Razon { get; set; }

        [BsonElement("Estado")]
        public string Estado { get; set; }

        [BsonElement("FechaHora")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaHora { get; set; }

        [BsonElement("Activo")]
        public bool Activo { get; set; }

        [BsonElement("paciente_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PacienteId { get; set; }

        [BsonElement("personalMedico_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PersonalMedicoId { get; set; }

        [BsonElement("Fecha de Modificacion")]
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;
        }
    } 
}