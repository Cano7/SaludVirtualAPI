using MongoDB.Driver;
using SaludVirtualAPI.DTOs;
using SaludVirtualAPI.Models;
using SaludVirtualAPI.Models.SaludVirtualAPI.Models;

namespace SaludVirtualAPI.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
            _database = client.GetDatabase("SaludVirtualDB");
        }

        public IMongoCollection<Paciente> Pacientes => _database.GetCollection<Paciente>("Pacientes");

        public IMongoCollection<PersonalMedico> PersonalMedico => _database.GetCollection<PersonalMedico>("PersonalMedico");

        public IMongoCollection<Personas> Personas => _database.GetCollection<Personas>("Personas");
        public IMongoCollection<Citas> Citas => _database.GetCollection<Citas>("Citas");

        public IMongoCollection<Tutor> Tutores => _database.GetCollection<Tutor>("Tutores");

        public IMongoCollection<Dependencias> Dependencias => _database.GetCollection<Dependencias>("Dependencias");
        public IMongoCollection<Cargos> Cargos => _database.GetCollection<Cargos>("Cargos");

        public IMongoCollection<HistorialMedico> HistorialMedico => _database.GetCollection<HistorialMedico>("HistorialMedico");


    }
}