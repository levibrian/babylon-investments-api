using System.Threading.Tasks;
using Babylon.Investments.Domain.Contracts.Requests.Base;

namespace Babylon.Investments.Domain.Contracts.Services
{
    public interface IUpdateableAsyncService<in TRequest> where TRequest : Request
    {
        /// <summary>
        /// Updates a given entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The entity Id.</returns>
        Task<long> UpdateAsync(TRequest entity);
    }
}
