using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class FishingSpot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Coordinates { get; set; }
        public List<string> FishTypes { get; set; }
        public float Rating { get; set; }
    }
}
