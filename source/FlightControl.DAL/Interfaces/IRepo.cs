using FlightControl.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.DAL.Interfaces
{
    public interface IRepo<T> : IDisposable where T: EntityBase, new()
    {
        int SaveChanges();
        T? GetOne(int id);
        List<T> GetAll();
        int Add(T entity);
        int AddRange(IList<T> entities);
        int Save(T entity);
        int Remove(int id);
        int Remove(T entity);
    }
}
