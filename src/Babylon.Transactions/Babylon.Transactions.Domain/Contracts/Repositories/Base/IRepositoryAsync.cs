using System.Collections.Generic;
using System.Threading.Tasks;

namespace Babylon.Transactions.Domain.Contracts.Repositories.Base
{
    /// <summary>
    /// The asynchronous repository interface.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Loads records asynchronously.
        /// </summary>
        /// <returns>The query results.</returns>
        Task<IEnumerable<T>> LoadAsync();
        
        /// <summary>
        /// Queries the database asynchronously
        /// </summary>
        /// <param name="key">The key to filter the results by</param>
        /// <returns>The query results.</returns>
        Task<IEnumerable<T>> QueryAsync(object key);

        /// <summary>
        /// Queries the database asynchronously.
        /// </summary>
        /// <param name="key">The partition key</param>
        /// <param name="sortKey">The sort/range key</param>
        /// <returns></returns>
        Task<T> QuerySingleAsync(object key, object sortKey);

        /// <summary>
        /// Saves a single record asynchronously. If the record is new, then it will be created. If it already exists, then it will be updated.
        /// </summary>
        /// <param name="entity">The record to save.</param>
        /// <returns>A task.</returns>
        Task SaveAsync(T entity);

        /// <summary>
        /// Saves a multiple records asynchronously. If the record is new, then it will be created. If it already exists, then it will be updated.
        /// </summary>
        /// <param name="entities">The records to save.</param>
        /// <returns>A task.</returns>
        Task SaveAsync(IEnumerable<T> entities);

        /// <summary>
        /// Deletes a single record asynchronously.
        /// </summary>
        /// <param name="entity">The record to delete.</param>
        /// <returns>A task.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Deletes multiple records asynchronously.
        /// </summary>
        /// <param name="entities">The records to delete.</param>
        /// <returns>A task.</returns>
        Task DeleteAsync(IEnumerable<T> entities);
    }
}
