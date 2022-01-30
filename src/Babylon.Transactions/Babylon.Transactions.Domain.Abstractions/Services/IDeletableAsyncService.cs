using System.Threading.Tasks;
using Babylon.Transactions.Domain.Abstractions.Dtos;
using Babylon.Transactions.Shared.Notifications;

namespace Babylon.Transactions.Domain.Abstractions.Services
{
    public interface IDeletableAsyncService<in TDto>
    {
        /// <summary>
        /// Deletes a previously created entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>The result code of the operation.</returns>
        Task<Result> DeleteAsync(TDto entity);
    }
}