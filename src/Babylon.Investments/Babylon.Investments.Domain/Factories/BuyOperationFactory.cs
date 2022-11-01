using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Abstractions.Handlers;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Handlers;

namespace Babylon.Investments.Domain.Factories
{
    public class BuyOperationFactory : IOperationFactory
    {
        private readonly ITransactionRepository _transactionRepository;
        
        public BuyOperationFactory(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        
        public bool AppliesTo(TransactionTypeEnum transactionType) => transactionType == TransactionTypeEnum.Buy;
        
        public IOperationHandler Create()
        {
            return new BuyOperationHandler(_transactionRepository);
        }
    }
}