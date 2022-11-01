using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Handlers;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Objects;
using Babylon.Investments.Domain.Objects.Base;

namespace Babylon.Investments.Domain.Handlers
{
    public interface IOperationHandler : IHandler<TransactionCreate, TransactionPostRequest>
    {
    }
}