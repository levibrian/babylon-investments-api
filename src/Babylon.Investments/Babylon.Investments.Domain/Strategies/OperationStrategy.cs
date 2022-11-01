using System;
using System.Linq;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Factories;
using Babylon.Investments.Domain.Handlers;

namespace Babylon.Investments.Domain.Strategies
{
    public interface IOperationStrategy
    {
        IOperationHandler Create(TransactionTypeEnum transactionType);
    }
    
    public class OperationStrategy : IOperationStrategy
    {
        private readonly IOperationFactory[] _operationFactories;

        public OperationStrategy(IOperationFactory[] operationFactories)
        {
            _operationFactories = operationFactories;
        }

        public IOperationHandler Create(TransactionTypeEnum transactionType)
        {
            var handlerFactory = _operationFactories
                .FirstOrDefault(of => of.AppliesTo(transactionType));

            if (handlerFactory is null)
            {
                throw new InvalidOperationException($"Operation: {transactionType.ToString()} not supported.");
            }

            return handlerFactory.Create();
        }
    }
}