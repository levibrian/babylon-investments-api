using Ivas.Core.Dtos.Base;
using Ivas.Entities.Base;
using System.Threading.Tasks;

namespace Ivas.Core.Services.Interfaces
{
    public interface IUpdateableAsyncService<T, TDto> where T : Entity
                                                     where TDto : Dto
    {
        /// <summary>
        /// Updates a given entity in the database.
        /// </summary>
        /// <param name="dto">The entity to update.</param>
        /// <returns>The entity Id.</returns>
        Task<long> UpdateAsync(TDto entity);
    }
}
