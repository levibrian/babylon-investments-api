using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class AreUnitsPositive : ISpecification<TransactionCreate>
    {
        public bool IsSatisfiedBy(TransactionCreate entityToEvaluate)
        {
            var expression = entityToEvaluate.Units > 0;

            if (!expression)
            {
                entityToEvaluate.DomainErrors.Add("Units are not positive. Please enter a valid value.");
            }
            
            return expression;
        }
    }
}