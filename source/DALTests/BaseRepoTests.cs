using Microsoft.EntityFrameworkCore;
using FlightControl.DAL.Models;
using FlightControl.DAL;

namespace DALTests
{
    public abstract class BaseRepoTests<T> where T : EntityBase, new()
    {
        private readonly FlightDbContext _dbContext;
        private readonly BaseRepo<T> _repo;
        private readonly int entityCount;

        protected DbContext Context => _dbContext;
        protected BaseRepo<T> Repo => _repo;
        public BaseRepoTests(int entityCount)
        {
            var options = new DbContextOptionsBuilder<FlightDbContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FlightControlTest")
                .Options;

            _dbContext = new FlightDbContext(options);
            TestDataInitializer.RecreateDatabase(_dbContext);

            this.entityCount = entityCount;
            _repo = CreateRepo(_dbContext);
        }

        protected abstract BaseRepo<T> CreateRepo(FlightDbContext dbContext);

        [TestMethod]
        public virtual void GetOneShouldReturnEntityWhenIdExists()
        {
            T? entity = _repo.GetOne(1);
            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public virtual void GetOneShouldReturnNullWhenIdDoesNotExist()
        {
            T? entity = _repo.GetOne(1000);
            Assert.IsNull(entity);
        }

        [TestMethod]
        public virtual void GetAllTest()
        {
            List<T> entities = _repo.GetAll();
            Assert.AreEqual(entityCount, entities.Count);
        }

        [TestMethod]
        public abstract void AddTest();

        [TestMethod]
        public abstract void AddRangeTest();

        [TestMethod]
        public abstract void SaveTest();

        [TestMethod]
        public void RemoveTestUsingEntity()
        {
            T? entity = _repo.GetOne(1);
            Assert.IsNotNull(entity);
            _repo.Remove(entity);
            entity = _repo.GetOne(1);
            Assert.IsNull(entity);
        }

        [TestMethod]
        public virtual void RemoveTestUsingId()
        {
            int count = _repo.Remove(2);
            Assert.AreEqual(1, count);
            T? entity = _repo.GetOne(2);
            Assert.IsNull(entity);
        }

        [TestMethod]
        public virtual void RemoveNotExisitingUsingId()
        {
            int count = _repo.Remove(0);
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public abstract void ShouldThrowAnErrorWhenEntityWithNoRequiredPropertyAdded();

        [TestMethod]
        public abstract void ShouldThrowAnErrorWhenDuplicateAdded();
    }
}
