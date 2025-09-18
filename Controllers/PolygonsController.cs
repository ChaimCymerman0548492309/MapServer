using MapServer.Models;
using MapServer.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MapServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolygonsController : ControllerBase
    {
        private readonly PolygonService _polygonService;

        public PolygonsController(PolygonService polygonService)
        {
            _polygonService = polygonService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PolygonEntity>>> Get() =>
            await _polygonService.GetAsync();

        [HttpPost]
        public async Task<ActionResult<PolygonEntity>> Post([FromBody] PolygonDto dto)
        {
            // המרה ל־GeoJsonPolygon
            var coords = dto.Coordinates[0]
                .Select(c => new GeoJson2DGeographicCoordinates(c[0], c[1]))
                .ToList();

            var polygon = new PolygonEntity
            {
                Name = dto.Name,
                Geometry = new GeoJsonPolygon<GeoJson2DGeographicCoordinates>(
                    new GeoJsonPolygonCoordinates<GeoJson2DGeographicCoordinates>(
                        new GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>(coords)
                    )
                )
            };

            await _polygonService.CreateAsync(polygon);
            return CreatedAtAction(nameof(Get), new { id = polygon.Id }, polygon);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _polygonService.DeleteAsync(id);
            return NoContent();
        }
    }
}
