using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ivas.Persistency.Repository.Interfaces
{
    /// <summary>
    /// The asynchronous repository interface.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Gets a single record asynchronously. If the where statement is not found, it will return a null value.
        /// </summary>
        /// <param name="predicate">The where statement.</param>
        /// <param name="enableTracking">Enable EF Core tracking of entities.</param>
        /// <param name="ignoreQueryFilters">If true it ignores the query filters.</param>
        /// <returns>The first match found.</returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
                                    bool enableTracking = true,
                                    bool ignoreQueryFilters = false);

        /// <summary>
        /// Queries the database asynchronously.
        /// </summary>
        /// <param name="predicate">The where statement.</param>
        /// <param name="enableTracking">Enable EF Core tracking of entities.</param>
        /// <returns>The query results.</returns>
        Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> predicate = null,
                                    bool enableTracking = true);

        Task<T> Insert(T entity);

        Task Insert(params T[] entities);

        Task Insert(IEnumerable<T> entities);
    }
}
