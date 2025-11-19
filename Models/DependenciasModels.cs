namespace SaludVirtualAPI.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    namespace SaludVirtualAPI.Models
    {
        [BsonIgnoreExtraElements]
        public class Dependencias
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            [BsonElement("CodigoDependencia")]
            public string CodigoDependencia { get; set; }

            [BsonElement("NombreDependencia")]
            public string NombreDependencia { get; set; }

            [BsonElement("Activo")]
            public bool Activo { get; set; }

            [BsonElement("Fecha de Modificación")]
            [BsonIgnoreIfNull]
            public DateTime? FechaModificacion { get; set; }
        }
    }
}