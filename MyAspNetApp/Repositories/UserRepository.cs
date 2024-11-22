using FishingAndCyclingApp.Data;
using FishingAndCyclingApp.Models;

namespace FishingAndCyclingApp.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

    }
}
