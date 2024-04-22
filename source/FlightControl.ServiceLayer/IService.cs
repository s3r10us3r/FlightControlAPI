using FlightControl.DAL.Interfaces;
using FlightControl.DAL.Models;

namespace FlightControl.ServiceLayer
{
    public interface IService<E, R> where E: EntityBase, new() where R : IRepo<E>
    {
        int DeleteOne(int id);
        int DeleteOne(E entity);
        E? ReadOne(int id);
        List<E> ReadAll();
    }
}
