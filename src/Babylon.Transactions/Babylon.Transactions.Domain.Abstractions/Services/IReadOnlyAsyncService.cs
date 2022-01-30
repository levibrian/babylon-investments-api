using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Transactions.Domain.Abstractions.Dtos;

namespace Babylon.Transactions.Domain.Abstractions.Services
{
    public interface IReadOnlyAsyncService<TDto> where TDto : Dto
    {
        /// <summary>
        /// Asynchronous query to fetch all records from a specified entity.
        /// </summary>
        /// <returns>The list of entities.</returns>
        Task<IEnumerable<TDto>> GetAsync();

        /// <summary>
        /// Asynchronous query to fetch a given entity.
        /// </summary>
        /// <param name="id">The Id of the entity.</param>
        /// <returns>The entity.</returns>
        Task<TDto> GetByIdAsync(long id);
    }
}
