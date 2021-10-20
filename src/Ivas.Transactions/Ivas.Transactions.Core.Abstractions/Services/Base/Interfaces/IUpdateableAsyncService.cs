using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Dtos.Base;
using Ivas.Transactions.Persistency.Abstractions.Entities;

namespace Ivas.Transactions.Core.Abstractions.Services.Base.Interfaces
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
