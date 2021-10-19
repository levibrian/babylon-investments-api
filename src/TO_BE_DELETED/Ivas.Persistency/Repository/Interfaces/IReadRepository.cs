using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ivas.Persistency.Repository.Interfaces
{
    public interface IReadRepository<T> where T : class
    {
        /// <summary>
        /// Gets a single record synchronously. If the where statement is not found, it will return a null value.
        /// </summary>
        /// <param name="predicate">The where statement.</param>
        /// <param name="enableTracking">Enable EF Core tracking of entities.</param>
        /// <param name="ignoreQueryFilters">If true it ignores the query filters.</param>
        /// <returns>The first match found.</returns>
        T SingleOrDefault(Expression<Func<T, bool>> predicate = null,
            bool enableTracking = true);

        /// <summary>
        /// Queries the database synchronously.
        /// </summary>
        /// <param name="predicate">The where statement.</param>
        /// <param name="enableTracking">Enable EF Core tracking of entities.</param>
        /// <returns>The query results.</returns>
        IQueryable<T> Query(Expression<Func<T, bool>> predicate = null,
            bool enableTracking = true);
    }
}
