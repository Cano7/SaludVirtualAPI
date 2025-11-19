using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaludVirtualAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Paciente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CodigoPaciente")]
        public string CodigoPaciente { get; set; }

        [BsonElement("FechaNacimiento")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaNacimiento { get; set; }

        [BsonElement("Alergias")]
        public string Alergias { get; set; }

        [BsonElement("Activo")]
        public bool Activo { get; set; }

        [BsonElement("persona_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PersonaId { get; set; }

        [BsonElement("tutor_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TutorId { get; set; }

        [BsonElement("Fecha de Modificación")]
        [BsonIgnoreIfNull]
        public DateTime? FechaModificacion { get; set; }
    }
}