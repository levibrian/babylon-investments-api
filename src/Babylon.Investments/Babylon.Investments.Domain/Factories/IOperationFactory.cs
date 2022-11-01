using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Handlers;

namespace Babylon.Investments.Domain.Factories
{
    public interface IOperationFactory
    {
        bool AppliesTo(TransactionTypeEnum transactionType);

        IOperationHandler Create();
    }
}