using System;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules.Primitives
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