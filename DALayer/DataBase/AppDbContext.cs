
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DALayer.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BikeRoute> BikeRoutes { get; set; }
        public DbSet<FishingSpot> FishingSpots { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
