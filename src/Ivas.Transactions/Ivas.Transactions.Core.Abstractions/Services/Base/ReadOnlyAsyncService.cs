using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ivas.Transactions.Core.Abstractions.Services.Base.Interfaces;
using Ivas.Transactions.Domain.Abstractions.Dtos.Base;
using Ivas.Transactions.Persistency.Abstractions.Entities;
using Ivas.Transactions.Persistency.Abstractions.UnitOfWork.Interfaces;

namespace Ivas.Transactions.Core.Abstractions.Services.Base
{
    /// <summary>
    /// The BaseAsyncService abstract class.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    /// <typeparam name="TDto">Type of dto.</typeparam>
    public abstract class ReadOnlyAsyncService<T, TDto> : IReadOnlyAsyncService<T, TDto> where T : Entity 
                                                                                         where TDto : Dto
    {
        /// <summary>
        /// The unit of work.
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// The mapper profile.
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        /// The BaseAsyncService constructor.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper profile.</param>
        protected ReadOnlyAsyncService(IUnitOfWork unitOfWork,
                                IMapper mapper)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Asynchronous query to fetch all records from a specified entity.
        /// </summary>
        /// <returns>The list of entities.</returns>
        public virtual async Task<IEnumerable<TDto>> GetAsync()
        {
            return Mapper.Map<IEnumerable<T>, IEnumerable<TDto>>(await UnitOfWork.RepositoryAsync<T>().QueryAsync());
        }

        /// <summary>
        /// Asynchronous query to fetch a given entity.
        /// </summary>
        /// <param name="id">The Id of the entity.</param>
        /// <returns>The entity.</returns>
        public virtual async Task<TDto> GetByIdAsync(long id)
        {
            return Mapper.Map<T, TDto>(await UnitOfWork.RepositoryAsync<T>().SingleOrDefaultAsync(x => x.Id == id));
        }
    }
}
