using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        IEnumerable<T> ListAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
