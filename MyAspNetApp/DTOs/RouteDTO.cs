namespace FishingAndCyclingApp.DTOs
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string Difficulty { get; set; }
    }

    public class CreateRouteDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string Difficulty { get; set; }
    }

    public class UpdateRouteDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string Difficulty { get; set; }
    }
}
