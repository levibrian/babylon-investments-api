using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsPricePositive : ISpecification<TransactionCreate>
    {
        public bool IsSatisfiedBy(TransactionCreate entityToEvaluate)
        {
            var expression = entityToEvaluate.PricePerUnit > 0;

            if (!expression)
            {
                entityToEvaluate.DomainErrors.Add("Price is not positive. Please enter a valid price.");
            }
            return expression;
        }
    }
}