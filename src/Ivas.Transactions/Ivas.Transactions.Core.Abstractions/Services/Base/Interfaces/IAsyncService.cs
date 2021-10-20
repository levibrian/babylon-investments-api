using Ivas.Transactions.Domain.Abstractions.Dtos.Base;
using Ivas.Transactions.Persistency.Abstractions.Entities;

namespace Ivas.Transactions.Core.Abstractions.Services.Base.Interfaces
{
    public interface IAsyncService<T, TDto> : ICreatableAsyncService<T, TDto>, 
                                              IUpdateableAsyncService<T, TDto>, 
                                              IReadOnlyAsyncService<T, TDto> 
        where TDto : Dto
        where T : Entity
    {
    }
}
