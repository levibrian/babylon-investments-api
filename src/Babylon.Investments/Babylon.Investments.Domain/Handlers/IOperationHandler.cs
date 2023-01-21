using Babylon.Investments.Domain.Abstractions.Handlers;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Objects;

namespace Babylon.Investments.Domain.Handlers
{
    public interface IOperationHandler : IHandler<BuyOperation, TransactionPostRequest>
    {
    }
}