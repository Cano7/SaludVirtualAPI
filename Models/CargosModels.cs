using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaludVirtualAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Cargos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CodigoCargo")]
        public string CodigoCargo { get; set; }

        [BsonElement("NombreCargo")]
        public string NombreCargo { get; set; }

        [BsonElement("Activo")]
        public bool Activo { get; set; }

        [BsonElement("Fecha de Modificación")]
        [BsonIgnoreIfNull]
        public DateTime? FechaModificacion { get; set; }
    }
}