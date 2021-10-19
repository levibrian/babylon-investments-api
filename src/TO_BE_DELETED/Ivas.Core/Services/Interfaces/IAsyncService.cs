using Ivas.Core.Dtos.Base;
using Ivas.Entities.Base;
using System.Threading.Tasks;

namespace Ivas.Core.Services.Interfaces
{
    public interface IAsyncService<T, TDto> : ICreatableAsyncService<T, TDto>, 
                                              IUpdateableAsyncService<T, TDto>, 
                                              IReadOnlyAsyncService<T, TDto> 
        where TDto : Dto
        where T : Entity
    {
    }
}
