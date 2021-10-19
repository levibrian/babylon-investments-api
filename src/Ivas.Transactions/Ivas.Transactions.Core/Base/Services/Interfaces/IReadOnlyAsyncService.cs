using System.Collections.Generic;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Dtos.Base;
using Ivas.Transactions.Domain.Abstractions.Entities;

namespace Ivas.Transactions.Core.Base.Services.Interfaces
{
    public interface IReadOnlyAsyncService<T, TDto> where T : Entity
                                                    where TDto : Dto
    {
        /// <summary>
        /// Asynchronous query to fetch all records from a specified entity.
        /// </summary>
        /// <returns>The list of entities.</returns>
        Task<IEnumerable<TDto>> GetAsync();

        // /// <summary>
        // /// Asynchronous query to fetch a given entity.
        // /// </summary>
        // /// <param name="id">The Id of the entity.</param>
        // /// <returns>The entity.</returns>
        // Task<TDto> GetByIdAsync(long id);
    }
}
