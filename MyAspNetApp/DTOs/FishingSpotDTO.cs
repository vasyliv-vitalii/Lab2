namespace MyAspNetApp.DTOs
{
    public class FishingSpotDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Coordinates { get; set; }
        public List<string> FishTypes { get; set; }
        public float Rating { get; set; }
    }

    public class CreateUpdateFishingSpotDto
    {
        public string Name { get; set; }
        public string Coordinates { get; set; }
        public List<string> FishTypes { get; set; }
        public float Rating { get; set; }
    }
}
