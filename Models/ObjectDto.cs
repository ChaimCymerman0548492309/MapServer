namespace MapServer.Models
{
    public class ObjectDto
    {
        public string Type { get; set; } = "Marker";
        public double[] Coordinates { get; set; } // [lon, lat]
    }
}
