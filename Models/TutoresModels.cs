using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaludVirtualAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Tutor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CodigoTutor")]
        public string CodigoTutor { get; set; }

        [BsonElement("Ocupacion")]
        public string Ocupacion { get; set; }

        [BsonElement("Active")]
        public bool Active { get; set; }

        [BsonElement("persona_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PersonaId { get; set; }

        [BsonElement("Fecha de Modificación")]
        [BsonIgnoreIfNull]
        public DateTime? FechaModificacion { get; set; }
    }
}