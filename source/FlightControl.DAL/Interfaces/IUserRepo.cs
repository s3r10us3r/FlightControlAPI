using FlightControl.DAL.Models;

namespace FlightControl.DAL.Interfaces
{
    public interface IUserRepo : IRepo<User>
    {
        User? GetOne(string login);
    }
}
