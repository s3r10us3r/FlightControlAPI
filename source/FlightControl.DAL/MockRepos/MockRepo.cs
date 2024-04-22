using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;

namespace FlightControl.DAL.MockRepos
{
    public abstract class MockRepo<E> : IRepo<E> where E : EntityBase, new()
    {
        protected List<E> _db;
        private int idCounter;

        public MockRepo()
        {
            _db = new(Initiate());
            idCounter = _db.Count + 1;
        }

        protected abstract List<E> Initiate();

        public int Add(E entity)
        {
            entity.Id = idCounter++;
            _db.Add(entity);
            return 1;
        }

        public int AddRange(IList<E> entities)
        {
            foreach (var e in entities)
            {
                _db.Add(e);
            }
            return entities.Count;
        }

        public void Dispose()
        {
        }

        public List<E> GetAll()
        {
            return _db;
        }

        public E? GetOne(int id)
        {
            return _db.Find(e => e.Id == id);
        }


        public int Remove(int id)
        {
            return _db.RemoveAll(e => e.Id == id);
        }

        public int Remove(E entity)
        {
            return Remove(entity.Id);
        }

        public int Save(E entity)
        {
            if (entity.Id < 100)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
