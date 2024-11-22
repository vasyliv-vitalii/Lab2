using FishingAndCyclingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FishingAndCyclingApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<FishingSpot> FishingSpots { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
