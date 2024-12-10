namespace MyAspNetApp.DTOs
{
    public class BikeRouteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string Difficulty { get; set; }
    }

    public class CreateUpdateBikeRouteDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string Difficulty { get; set; }
    }
}
