using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;


namespace FlightControl.ServiceLayer
{
    public abstract class BaseService<E, R> : IService<E, R> where E : EntityBase, new() where R : IRepo<E>  
    {
        protected readonly R _repo;

        public BaseService(R repo)
        {
            _repo = repo;
        }

        public int DeleteOne(int id)
        {
            return _repo.Remove(id);
        }

        public int DeleteOne(E entity)
        {
            return _repo.Remove(entity);
        }

        public E? ReadOne(int id)
        {
            return _repo.GetOne(id);
        }

        public List<E> ReadAll()
        {
            return _repo.GetAll().ToList();
        }

        public virtual E Update(E entity)
        {
            _repo.Save(entity);
            return entity;
        }
    }
}
