using System.Threading.Tasks;
using Babylon.Investments.Domain.Contracts.Dtos;
using Babylon.Investments.Domain.Contracts.Dtos.Base;

namespace Babylon.Investments.Domain.Contracts.Services
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
