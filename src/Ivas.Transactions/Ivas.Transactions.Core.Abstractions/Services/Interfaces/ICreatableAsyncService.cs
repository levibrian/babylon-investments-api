using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Domain.Abstractions.Dtos.Base;
using Ivas.Transactions.Persistency.Abstractions.Entities;

namespace Ivas.Transactions.Core.Abstractions.Services.Interfaces
{
    public interface ICreatableAsyncService<T, TDto> where T : Entity
                                                     where TDto : Dto
    {
        /// <summary>
        /// Creates and persists a given entity in the database.
        /// </summary>
        /// <param name="dto">The entity/dto to persist.</param>
        /// <returns>The entity Id.</returns>
        Task<long> CreateAsync(TDto entity);

        /// <summary>
        /// Deletes a previously created entity.
        /// </summary>
        /// <param name="id">The Id of the entity to be deleted.</param>
        /// <returns>The result code of the operation.</returns>
        Task<long> DeleteAsync(long id);
    }
}
