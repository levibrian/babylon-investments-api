using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsTickerProvided : ISpecification<TransactionCreate>
    {
        public bool IsSatisfiedBy(TransactionCreate entityToEvaluate)
        {
            return !string.IsNullOrEmpty(entityToEvaluate.Ticker) && 
                   !string.IsNullOrWhiteSpace(entityToEvaluate.Ticker);
        }
    }
}