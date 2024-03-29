﻿using System.Threading.Tasks;
using Babylon.Investments.Shared.Notifications;

namespace Babylon.Investments.Domain.Abstractions.Services.Base
{
    public interface IDeletableAsyncService<in TRequest>
    {
        /// <summary>
        /// Deletes a previously created entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>The result code of the operation.</returns>
        Task<Result> DeleteAsync(TRequest entity);
    }
}