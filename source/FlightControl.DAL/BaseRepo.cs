using FlightControl.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightControl.DAL
{
    public abstract class BaseRepo<T> : IDisposable where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly FlightDbContext _db;

        public BaseRepo(FlightDbContext dbContext)
        {
            _db = dbContext;
            _table = _db.Set<T>();
        }

        protected FlightDbContext Context => _db;
        protected DbSet<T> Table => _table;

        public void Dispose()
        {
            _db?.Dispose();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public T? GetOne(int id)
        {
            return _table.Find(id);
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Save(T entity)
        {
            _table.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        public int Remove(int id)
        {
            T? entity = _table.Find(id);
            if (entity is null)
            {
                return 0;
            }
            _table.Remove(entity);
            return SaveChanges();
        }

        public int Remove(T entity)
        {
            _table.Remove(entity);
            return SaveChanges();
        }
    }
}
