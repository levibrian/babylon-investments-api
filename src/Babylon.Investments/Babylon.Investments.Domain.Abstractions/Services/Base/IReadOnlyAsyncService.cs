using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Requests.Base;

namespace Babylon.Investments.Domain.Abstractions.Services.Base
{
    public interface IReadOnlyAsyncService<TRequest> where TRequest : Request
    {
        /// <summary>
        /// Asynchronous query to fetch all records from a specified entity.
        /// </summary>
        /// <returns>The list of entities.</returns>
        Task<IEnumerable<TRequest>> GetAsync();

        /// <summary>
        /// Asynchronous query to fetch a given entity.
        /// </summary>
        /// <param name="id">The Id of the entity.</param>
        /// <returns>The entity.</returns>
        Task<TRequest> GetByIdAsync(long id);
    }
}
