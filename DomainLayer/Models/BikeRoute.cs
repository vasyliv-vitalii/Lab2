using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class BikeRoute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string Difficulty { get; set; }
        public List<string> Locations { get; set; } = new List<string>();
    }
}
