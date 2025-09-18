namespace MapServer.Models
{
    public class PolygonDto
    {
        public string Name { get; set; } = string.Empty;
        public List<List<List<double>>> Coordinates { get; set; }
    }
}
