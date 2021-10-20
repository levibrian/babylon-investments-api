using System.Threading.Tasks;
using AutoMapper;
using Ivas.Transactions.Core.Abstractions.Services.Base.Interfaces;
using Ivas.Transactions.Domain.Abstractions.Dtos.Base;
using Ivas.Transactions.Persistency.Abstractions.Entities;
using Ivas.Transactions.Persistency.Abstractions.UnitOfWork.Interfaces;

namespace Ivas.Transactions.Core.Abstractions.Services.Base
{
    /// <summary>
    /// The AsyncService abstract class.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    /// <typeparam name="TDto">Type of dto.</typeparam>
    public abstract class AsyncService<T, TDto> : ReadOnlyAsyncService<T, TDto>, IAsyncService<T, TDto> where TDto : Dto
                                                                                                        where T : Entity
    {
        /// <summary>
        /// The AsyncService constructor.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper profile.</param>
        protected AsyncService(IUnitOfWork unitOfWork, 
            IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        /// <summary>
        /// Creates and persists a given entity in the database.
        /// </summary>
        /// <param name="dto">The entity/dto to persist.</param>
        /// <returns>The entity Id.</returns>
        public abstract Task<long> CreateAsync(TDto dto);

        /// <summary>
        /// Deletes a previously created entity.
        /// </summary>
        /// <param name="id">The Id of the entity to be deleted.</param>
        /// <returns>The result code of the operation.</returns>
        public abstract Task<long> DeleteAsync(long id);

        /// <summary>
        /// Updates a given entity in the database.
        /// </summary>
        /// <param name="dto">The entity to update.</param>
        /// <returns>The entity Id.</returns>
        public abstract Task<long> UpdateAsync(TDto dto);
    }
}
