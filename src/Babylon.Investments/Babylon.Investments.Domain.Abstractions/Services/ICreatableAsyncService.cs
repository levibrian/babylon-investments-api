using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Dtos;
using Babylon.Investments.Shared.Notifications;

namespace Babylon.Investments.Domain.Abstractions.Services
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
