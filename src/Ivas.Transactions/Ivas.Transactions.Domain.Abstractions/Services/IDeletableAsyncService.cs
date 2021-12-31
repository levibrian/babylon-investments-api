using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Shared.Notifications;

namespace Ivas.Transactions.Domain.Abstractions.Services
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