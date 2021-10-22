using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsTickerProvided : ISpecification<TransactionCreate>
    {
        public bool IsSatisfiedBy(TransactionCreate entityToEvaluate)
        {
            var expression = entityToEvaluate.Ticker != null && 
                             !string.IsNullOrEmpty(entityToEvaluate.Ticker) &&
                             !string.IsNullOrWhiteSpace(entityToEvaluate.Ticker);

            if (!expression)
            {
                entityToEvaluate.DomainErrors.Add("Ticker is not provided.");
            }
            
            return expression;
        }
    }
}