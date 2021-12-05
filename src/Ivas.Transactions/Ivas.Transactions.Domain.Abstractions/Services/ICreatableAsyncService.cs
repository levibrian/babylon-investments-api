using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Shared.Notifications;

namespace Ivas.Transactions.Domain.Abstractions.Services
{
    public interface ICreatableAsyncService<in TDto>
    {
        /// <summary>
        /// Creates and persists a given entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The entity Id.</returns>
        Task<Result> CreateAsync(TDto entity);
    }
}
