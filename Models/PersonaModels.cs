using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaludVirtualAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Personas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Identificacion")]
        public string identificacion { get; set; }

        [BsonElement("PrimerNombre")]
        public string PrimerNombre { get; set; }

        [BsonElement("SegundoNombre")]
        public string SegundoNombre { get; set; }

        [BsonElement("Apellido")]
        public string Apellido { get; set; }

        [BsonElement("Sexo")]
        public string Sexo { get; set; }

        [BsonElement("Edad")]
        public int Edad { get; set; }

        [BsonElement("Telefono")]
        public object Telefono { get; set; }

        [BsonElement("Correo")]
        public string Correo { get; set; }

        [BsonElement("Direccion")]
        public string Direccion { get; set; }

        [BsonIgnore]
        public int TelefonoInt
        {
            get
            {
                if (Telefono is int telefonoInt)
                    return telefonoInt;
                if (Telefono is string telefonoStr && int.TryParse(telefonoStr, out int result))
                    return result;
                return 0;
            }
        }
    }
}