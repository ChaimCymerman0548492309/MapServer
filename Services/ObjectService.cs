using MapServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MapServer.Services
{
    public class ObjectService
    {
        private readonly IMongoCollection<ObjectEntity> _objects;

        public ObjectService(IOptions<MongoDbSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _objects = database.GetCollection<ObjectEntity>("objects");
        }

        public async Task<List<ObjectEntity>> GetAsync() =>
            await _objects.Find(_ => true).ToListAsync();

        public async Task<ObjectEntity> CreateAsync(ObjectEntity obj)
        {
            await _objects.InsertOneAsync(obj);
            return obj;
        }

        public async Task DeleteAsync(string id) =>
            await _objects.DeleteOneAsync(o => o.Id == id);
    }
}
