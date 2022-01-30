using Babylon.Transactions.Domain.Enums;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Domain.Rules.Primitives
{
    public class IsStringNumberValid : ISpecification<string>
    {
        public bool IsPrimitiveSatisfiedBy(string stringToEvaluate)
        {
            var expression = 
                !string.IsNullOrWhiteSpace(stringToEvaluate) && 
                int.TryParse(stringToEvaluate, out var convertedInt) &&
                convertedInt > 0;

            return expression;
        }
    }
}