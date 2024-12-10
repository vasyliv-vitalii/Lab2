using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<BikeRoute> SubscribedRoutes { get; set; } = new List<BikeRoute>();
        public List<FishingSpot> SubscribedFishingSpots { get; set; } = new List<FishingSpot>();
    }
}