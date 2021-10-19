using Ivas.Transactions.Domain.Abstractions.Dtos.Base;
using Ivas.Transactions.Domain.Abstractions.Entities;

namespace Ivas.Transactions.Core.Base.Services.Interfaces
{
    public interface IAsyncService<T, TDto> : ICreatableAsyncService<T, TDto>, 
                                              IUpdateableAsyncService<T, TDto>, 
                                              IReadOnlyAsyncService<T, TDto> 
        where TDto : Dto
        where T : Entity
    {
    }
}
