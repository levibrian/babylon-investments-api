using Ivas.Persistency.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ivas.Persistency.Repository
{
    /// <summary>
    /// The asynchronous Repository class.
    /// </summary>
    /// <typeparam name="T">The specified entity.</typeparam>
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// The table accessor.
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// The async repository constructor.
        /// </summary>
        /// <param name="dbContext">The EF database context.</param>
        public RepositoryAsync(DbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Gets a single record asynchronously. If the where statement is not found, it will return a null value.
        /// </summary>
        /// <param name="predicate">The where statement.</param>
        /// <param name="enableTracking">Enable EF Core tracking of entities.</param>
        /// <param name="ignoreQueryFilters">If true it ignores the query filters.</param>
        /// <returns>The first match found.</returns>
        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
                                                          bool enableTracking = true,
                                                          bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Queries the database asynchronously.
        /// </summary>
        /// <param name="predicate">The where statement.</param>
        /// <param name="enableTracking">Enable EF Core tracking of entities.</param>
        /// <returns>The query results.</returns>
        public async Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> predicate = null,
                                                    bool enableTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            await query.LoadAsync();

            return query;
        }

        public async Task<T> Insert(T entity)
        {
            var addOp = await _dbSet.AddAsync(entity);

            return addOp.Entity;                    
        }

        public async Task Insert(params T[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task Insert(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
    }
}
