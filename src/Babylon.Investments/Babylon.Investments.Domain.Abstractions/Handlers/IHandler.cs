using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Requests.Base;

namespace Babylon.Investments.Domain.Abstractions.Handlers
{
    public interface IHandler<T, in TV>
    {
        Task<T> HandleAsync(TV request);
    }
}