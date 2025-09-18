using MapServer.Models;
using MapServer.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MapServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjectsController : ControllerBase
    {
        private readonly ObjectService _objectService;

        public ObjectsController(ObjectService objectService)
        {
            _objectService = objectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ObjectEntity>>> Get() =>
            await _objectService.GetAsync();

        [HttpPost]
        public async Task<ActionResult<ObjectEntity>> Post([FromBody] ObjectDto dto)
        {
            var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                new GeoJson2DGeographicCoordinates(dto.Coordinates[0], dto.Coordinates[1])
            );

            var obj = new ObjectEntity
            {
                Type = dto.Type,
                Location = point
            };

            await _objectService.CreateAsync(obj);
            return CreatedAtAction(nameof(Get), new { id = obj.Id }, obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _objectService.DeleteAsync(id);
            return NoContent();
        }
    }
}



