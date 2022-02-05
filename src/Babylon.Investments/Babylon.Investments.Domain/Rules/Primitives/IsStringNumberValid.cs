using Babylon.Investments.Domain.Enums;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules.Primitives
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