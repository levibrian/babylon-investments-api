using System.Threading.Tasks;

namespace Babylon.Investments.Domain.Abstractions.Handlers
{
    public interface IHandler<T, in TV>
    {
        Task<T> HandleAsync(TV request);
    }
}