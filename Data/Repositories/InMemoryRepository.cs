using BusinessEntities;
using Common;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{

    [AutoRegister()]
    public class InMemoryRepository<T> : IRepository<T> where T : IdObject
    {
        private readonly ICustomCache _cache;
        private readonly string _cacheKey;

        public InMemoryRepository(ICustomCache cache)
        {
            _cache = cache;
            _cacheKey = typeof(T).Name;
        }


        public async Task SaveAsync(T entity)
        {
            await Task.Run(() =>
            {
                var items = _cache.GetOrCreate(_cacheKey, entry => new List<T>());
                var existing = items.FirstOrDefault(e => e.Id == entity.Id);
                if (existing != null)
                {
                    items.Remove(existing);
                }
                items.Add(entity);
                _cache.Set(_cacheKey, items);
            });
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                var items = _cache.Get<List<T>>(_cacheKey) ?? new List<T>();
                return items.FirstOrDefault(e => e.Id == id);
            });
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var items = _cache.Get<List<T>>(_cacheKey) ?? new List<T>();
                return items.AsEnumerable();
            });
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() =>
            {
                var items = _cache.Get<List<T>>(_cacheKey) ?? new List<T>();
                if (items.Contains(entity))
                {
                    items.Remove(entity);
                    _cache.Set(_cacheKey, items);
                }
            });
        }


    }
}
