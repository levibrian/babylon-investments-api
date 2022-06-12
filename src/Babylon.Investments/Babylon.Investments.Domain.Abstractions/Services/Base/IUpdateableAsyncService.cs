using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Requests.Base;

namespace Babylon.Investments.Domain.Abstractions.Services.Base
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
