using System;
using System.Linq;
using System.Linq.Expressions;
using Ivas.Transactions.Persistency.Abstractions.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ivas.Transactions.Persistency.Abstractions.Repository.Base
{
    /// <summary>
    /// The base repository class.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public abstract class BaseRepository<T> : IReadRepository<T> where T : class
    {
        /// <summary>
        /// The database context.
        /// </summary>
        protected readonly DbContext _dbContext;

        /// <summary>
        /// The database set table accessor.
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// The base repository constructor.
        /// </summary>
        /// <param name="context">The EF database context.</param>
        public BaseRepository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }

        /// <summary>
        /// Gets a single record synchronously. If the where statement is not found, it will return a null value.
        /// </summary>
        /// <param name="predicate">The where statement.</param>
        /// <param name="enableTracking">Enable EF Core tracking of entities.</param>
        /// <param name="ignoreQueryFilters">If true it ignores the query filters.</param>
        /// <returns>The first match found.</returns>
        public T SingleOrDefault(Expression<Func<T, bool>> predicate = null,
                                 bool enableTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            return query.FirstOrDefault();
        }


        /// <summary>
        /// Queries the database synchronously.
        /// </summary>
        /// <param name="predicate">The where statement.</param>
        /// <param name="enableTracking">Enable EF Core tracking of entities.</param>
        /// <returns>The query results.</returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> predicate = null,
                                   bool enableTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            return query;
        }
    }
}
