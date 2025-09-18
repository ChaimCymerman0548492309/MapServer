using MapServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MapServer.Services
{
    public class PolygonService
    {
        private readonly IMongoCollection<PolygonEntity> _polygons;

        public PolygonService(IOptions<MongoDbSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _polygons = database.GetCollection<PolygonEntity>("polygons");
        }

        public async Task<List<PolygonEntity>> GetAsync() =>
            await _polygons.Find(_ => true).ToListAsync();

        public async Task<PolygonEntity> CreateAsync(PolygonEntity polygon)
        {
            await _polygons.InsertOneAsync(polygon);
            return polygon;
        }

        public async Task DeleteAsync(string id) =>
            await _polygons.DeleteOneAsync(p => p.Id == id);
    }
}
