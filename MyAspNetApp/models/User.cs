using System.ComponentModel.DataAnnotations;

namespace FishingAndCyclingApp.Models
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
        public string Role { get; set; }
        public List<Route> SubscribedRoutes { get; set; } = new List<Route>();
        public List<FishingSpot> SubscribedFishingSpots { get; set; } = new List<FishingSpot>();
    }
}