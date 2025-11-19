using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaludVirtualAPI.Models
{
    [BsonIgnoreExtraElements]
    public class PersonalMedico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CodigoMedico")]
        public string CodigoMedico { get; set; }

        [BsonElement("Active")]
        public bool Active { get; set; }

        [BsonElement("persona_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PersonaId { get; set; }

        [BsonElement("cargo_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CargoId { get; set; }

        [BsonElement("dependencia_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DependenciaId { get; set; }

        [BsonElement("Fecha de Modificación")]
        public DateTime? FechaModificacion { get; set; }
    }
}