using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;

namespace FlightControl.DAL
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public UserRepo(FlightDbContext dbContext) : base(dbContext)
        {
        }

        public User? GetOne(string login)
        {
            return Table
                .Where(u => u.Login == login)
                .FirstOrDefault();
        }
    }
}
