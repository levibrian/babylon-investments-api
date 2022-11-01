using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Handlers;
using Babylon.Investments.Domain.Validators;

namespace Babylon.Investments.Domain.Factories
{
    public class SellOperationFactory : IOperationFactory
    {
        private readonly ITransactionRepository _transactionRepository;

        private readonly IOperationValidator _operationValidator;

        public SellOperationFactory(
            ITransactionRepository transactionRepository, 
            IOperationValidator operationValidator)
        {
            _transactionRepository = transactionRepository;
            _operationValidator = operationValidator;
        }

        public bool AppliesTo(TransactionTypeEnum transactionType) => transactionType == TransactionTypeEnum.Sell;

        public IOperationHandler Create()
        {
            return new SellOperationHandler(_transactionRepository, _operationValidator);
        }
    }
}