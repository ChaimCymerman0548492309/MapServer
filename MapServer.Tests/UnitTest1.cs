using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MapServer.Tests;

public class ServerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ServerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Polygons_Should_Return_OK()
    {
        var response = await _client.GetAsync("/api/polygons");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_Polygon_Should_Return_Created()
    {
        var polygon = new
        {
            name = "Test Polygon",
            coordinates = new[]
            {
                new[]
                {
                    new[] { 34.78, 32.07 },
                    new[] { 34.79, 32.07 },
                    new[] { 34.79, 32.08 },
                    new[] { 34.78, 32.08 },
                    new[] { 34.78, 32.07 }
                }
            }
        };

        var response = await _client.PostAsJsonAsync("/api/polygons", polygon);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Post_Object_Should_Return_Created()
    {
        var obj = new
        {
            type = "Marker",
            coordinates = new[] { 34.78, 32.07 }
        };

        var response = await _client.PostAsJsonAsync("/api/objects", obj);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}

// dotnet test