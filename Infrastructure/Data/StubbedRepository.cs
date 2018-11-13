using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Interfaces;

namespace Infrastructure.Data
{
    /// <summary>
    /// Stub for persistent storage, doesn't follow any sort of best practises
    /// </summary>
    /// <typeparam name="T">Type of entity to persist</typeparam>
    public class StubbedRepository<T> : IRepository<T>, IAsyncRepository<T> where T : Entity
    {
        private readonly ConcurrentDictionary<int, T> _storage = new ConcurrentDictionary<int, T>();

        public T Add(T entity)
        {
            _storage.TryAdd(entity.Id, entity);
            return entity;
        }

        public Task<T> AddAsync(T entity)
        {
            return new Task<T>(() =>
            {
                _storage.TryAdd(entity.Id, entity);
                return entity;
            });
        }

        public void Delete(T entity)
        {
            _storage.TryRemove(entity.Id, out var _);
        }

        public Task DeleteAsync(T entity)
        {
            return new Task(() =>
                _storage.TryRemove(entity.Id, out var _));
        }

        public T GetById(int id)
        {
            return _storage[id];
        }

        public Task<T> GetByIdAsync(int id)
        {
            return new Task<T>(() => _storage[id]);
        }

        public IEnumerable<T> ListAll()
        {
            return _storage.Values;
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            return new Task<IReadOnlyList<T>>(_storage.Values.ToList);
        }

        public void Update(T entity)
        {
            _storage.TryUpdate(entity.Id, entity, _storage[entity.Id]);
        }

        public Task UpdateAsync(T entity)
        {
            return new Task(() => _storage.TryUpdate(entity.Id, entity, _storage[entity.Id]));
        }
    }
}
