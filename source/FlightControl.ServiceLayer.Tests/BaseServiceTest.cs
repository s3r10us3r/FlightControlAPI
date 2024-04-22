using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;

namespace FlightControl.ServiceLayer.Tests
{
    public abstract class BaseServiceTest<E, R> where E : EntityBase, new() where R : IRepo<E>
    {
        protected readonly BaseService<E, R> _service;

        public BaseServiceTest(BaseService<E, R> service)
        {
            _service = service;
        }

        [TestMethod]
        public void DeleteOneWithIdShouldReturnOne()
        {
            int delete = _service.DeleteOne(2);
            Assert.AreEqual(1, delete);
        }

        [TestMethod]
        public abstract void DeleteOneWithEntityShouldReturnOne();

        [TestMethod]
        public void ReadOneShouldReturnEntity()
        {
            var entity = _service.ReadOne(3);
            Assert.IsNotNull(entity);
        }
    }
}
