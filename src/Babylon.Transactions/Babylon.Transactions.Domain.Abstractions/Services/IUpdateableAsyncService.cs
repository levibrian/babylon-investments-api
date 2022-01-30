using System.Threading.Tasks;
using Babylon.Transactions.Domain.Abstractions.Dtos;

namespace Babylon.Transactions.Domain.Abstractions.Services
{
    public interface IUpdateableAsyncService<in TDto> where TDto : Dto
    {
        /// <summary>
        /// Updates a given entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The entity Id.</returns>
        Task<long> UpdateAsync(TDto entity);
    }
}
