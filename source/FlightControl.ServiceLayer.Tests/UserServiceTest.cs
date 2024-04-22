using FlightControl.DAL.Interfaces;
using FlightControl.DAL.MockRepos;
using FlightControl.DAL.Models;

namespace FlightControl.ServiceLayer.Tests
{
    [TestClass]
    public class UserServiceTest : BaseServiceTest<User, IUserRepo>
    {
        static private readonly IUserRepo _repo = new MockUsers();

        public UserServiceTest() : base(new UserService(_repo))
        {
        }

        [TestMethod]
        public override void DeleteOneWithEntityShouldReturnOne()
        {
            User user = new() { Login = "TestUser1", Id = 1, PasswordHash = "TestPasswordHash" };
            int delete = _service.DeleteOne(user);
            Assert.AreEqual(1, delete);
        }

        [TestMethod]
        public void ChangePasswordShouldThrowExceptionForInvalidPassword()
        {
            User func() => ((UserService)_service).ChangePassword(new User { Id = 1, Login = "TestUser1" }, "");
            Assert.ThrowsException<InvalidOperationException>((Func<User>)func);
        }

        [TestMethod]
        public void ChangePasswordShoulReturnEntityForValidPassword()
        {
            User user = new() { Id = 1, Login = "UserName" };
            user = ((UserService)_service).ChangePassword(user, "ThisIsAPassword");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void AuthenticateUserShouldCorrectlyAuthenticate()
        {
            User verified = ((UserService)_service).AuthenticateUser("TestUser1", "TestUser1");
            Assert.IsNotNull(verified);
        }

        [TestMethod]
        public void AuthenticateUserShouldCorrectlyThrowWhenPasswordIsInvalid()
        {
            var func = () => ((UserService)_service).AuthenticateUser("TestUser1", "not a password");
            Assert.ThrowsException<InvalidOperationException>(func);
        }
    }
}
