using FlightControl.DAL;
using FlightControl.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DALTests
{
    [TestClass]
    public class UserRepoTests : BaseRepoTests<User>
    {
        public UserRepoTests() : base(3)
        {
        }

        [TestMethod]
        public override void AddRangeTest()
        {
            var users = new List<User>()
            {
                new() {Login = "AddedUser1", PasswordHash = "AddedPassword1"},
                new() {Login = "AddedUser2", PasswordHash = "AddedPassword2"},
                new() {Login = "AddedUser3", PasswordHash = "AddedPassword3"}
            };

            int changes = Repo.AddRange(users);
            Assert.AreEqual(3, changes);

            foreach (var user in users)
            {
                User? newUser = Repo.GetOne(user.Id);
                Assert.IsNotNull(newUser);
            }
        }

        [TestMethod]
        public override void AddTest()
        {
            var user = new User() { Login = "AddedUser1", PasswordHash = "AddedPassword1"};

            int changes = Repo.Add(user);
            Assert.AreEqual(1, changes);

            var newUser = Repo.GetOne(user.Id);
            Assert.IsNotNull(newUser);
        }

        [TestMethod]
        public override void SaveTest()
        {
            User? user = Repo.GetOne(1);
            Assert.IsNotNull(user);
            user.Login = "ChangedLogin";
            int count = Repo.Save(user);
            Assert.AreEqual(1, count);

            User? newUser = Repo.GetOne(1);
            Assert.IsNotNull(newUser);
            Assert.AreEqual(user.Login, newUser.Login);
        }

        [TestMethod]
        public void GetOneUsingLoginTest()
        {
            User? user = ((UserRepo)Repo).GetOne("TestUser1");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public override void ShouldThrowAnErrorWhenEntityWithNoRequiredPropertyAdded()
        {
            User user = new() { };

            Assert.ThrowsException<DbUpdateException>(() =>
            {
                Repo.Add(user);
            });
        }

        [TestMethod]
        public override void ShouldThrowAnErrorWhenDuplicateAdded()
        {
            User user = new() { Login = "TestUser1", PasswordHash = "NotHashedtestUser1"};

            Assert.ThrowsException<DbUpdateException>(() =>
            {
                Repo.Add(user);
            });
        }

        protected override BaseRepo<User> CreateRepo(FlightDbContext dbContext)
        {
            return new UserRepo(dbContext);
        }
    }
}
