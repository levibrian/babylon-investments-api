using System;
using Babylon.Transactions.Domain.Enums;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Domain.Rules.Primitives
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