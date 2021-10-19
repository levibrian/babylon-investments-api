using System;
using System.Collections.Generic;

namespace Ivas.Persistency.Repository.Interfaces
{
    public interface IRepository<T> : IReadRepository<T>, IDisposable where T : class
    {
        T Insert(T entity);

        void Insert(params T[] entities);

        void Insert(IEnumerable<T> entities);

        void Update(T entity);

        void Update(params T[] entities);

        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);
    }
}
