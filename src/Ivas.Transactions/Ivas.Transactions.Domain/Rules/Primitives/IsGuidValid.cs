using System;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules.Primitives
{
    public class IsGuidValid : ISpecification<string>
    {
        public bool IsPrimitiveSatisfiedBy(string stringToEvaluate)
        {
            var expression = 
                !string.IsNullOrWhiteSpace(stringToEvaluate) && 
                Guid.TryParse(stringToEvaluate, out var guidOutput);

            return expression;
        }
    }
}