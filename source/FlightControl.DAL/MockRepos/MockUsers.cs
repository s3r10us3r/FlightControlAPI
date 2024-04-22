using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;

namespace FlightControl.DAL.MockRepos
{
    public class MockUsers : MockRepo<User>, IUserRepo
    {
        public MockUsers() : base()
        {
        }

        public User? GetOne(string login)
        {
            return _db.Find(u => u.Login == login);
        }

        protected override List<User> Initiate()
        {
            return
            [
                new() {Id = 1, Login = "TestUser1", PasswordHash=BCrypt.Net.BCrypt.HashPassword("TestUser1")},
                new() {Id = 2, Login = "TestUser2", PasswordHash=BCrypt.Net.BCrypt.HashPassword("TestUser2")},
                new() {Id = 3, Login = "TestUser3", PasswordHash=BCrypt.Net.BCrypt.HashPassword("TestUser3")}
            ];
        }
    }
}
