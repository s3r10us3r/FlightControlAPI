using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Crypt = BCrypt.Net.BCrypt;

namespace FlightControl.ServiceLayer
{
    public class UserService : BaseService<User, IUserRepo>
    {
        public UserService(IUserRepo userRepo) : base(userRepo)
        {
        }
        
        public User ChangePassword(User user, string newPassword)
        {
            bool isPasswordValid = ValidatePassword(newPassword);
            if (!isPasswordValid)
            {
                throw new InvalidOperationException("Password is not valid!");
            }

            string hashedPassword = HashPassword(newPassword);
            user.PasswordHash = hashedPassword;
            Update(user);

            return user;
        }

        public User? ReadOne(string login)
        {
            return _repo.GetOne(login);
        }

        public int DeleteOne(string login)
        {
            User? user = _repo.GetOne(login);

            if (user is null)
            {
                return 0;
            }

            return _repo.Remove(user);
        }

        public User CreateOne(string login, string password)
        {
            ValidateLogin(login);

            bool isPasswordValid = ValidatePassword(password);
            if (isPasswordValid == false)
            {
                throw new InvalidOperationException("Password is too short!");
            }
            string hashedPassword = HashPassword(password);
            User user = new User() { Login = login, PasswordHash = hashedPassword};
            int count = _repo.Add(user);

            if (count >= 1)
            {
                return user;
            }
           
            throw new DbUpdateException("Failed to add user to the database!");
        }

        public User AuthenticateUser(string login, string password)
        {
            User? user = _repo.GetOne(login);

            if (user is null)
            {
                throw new InvalidOperationException("User does not exist in the database!");
            }

            bool passwordMatches = Crypt.Verify(password, user.PasswordHash);
            if (passwordMatches)
            {
                return user;
            }

            throw new InvalidOperationException("Password is invalid!");
        }

        private string HashPassword(string password)
        {
            string salt = Crypt.GenerateSalt();
            string hashedPassword = Crypt.HashPassword(password, salt);
            return hashedPassword;
        }

        private bool ValidatePassword(string password)
        {
            if (password is null || password.Length < 8)
            {
                return false;
            }

            return true;
        }

        private void ValidateLogin(string login)
        {
            if (login is null || login.Length < 6)
            {
                throw new InvalidOperationException("Login is too short!");
            }

            User? existing = _repo.GetOne(login);
            if (existing is not null)
            {
                throw new InvalidOperationException("Useer already exists!");
            }
        }
    }
}
