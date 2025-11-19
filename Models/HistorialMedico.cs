using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaludVirtualAPI.Models
{
    public class HistorialMedico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CodigoHistorialMedico")]
        public string CodigoHistorialMedico { get; set; }

        [BsonElement("Diagnostico")]
        public string Diagnostico { get; set; }

        [BsonElement("Tratamiento")]
        public string Tratamiento { get; set; }

        [BsonElement("Pronostico")]
        public string Pronostico { get; set; }

        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; }

        [BsonElement("PesoLibras")]
        public decimal PesoLibras { get; set; }

        [BsonElement("Medida")]
        public decimal Medida { get; set; }

        [BsonElement("Activo")]
        public bool Activo { get; set; }

        [BsonElement("Fecha de Modificacion")]
        public DateTime? FechaModificacion { get; set; }

        [BsonElement("paciente_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PacienteId { get; set; }

    }
}