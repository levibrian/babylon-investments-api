using System;
using System.Collections.Generic;
using System.Linq;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Base;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Shared.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(
            ISpecification<T> firstSpecification, 
            ISpecification<T> secondSpecification) : base(firstSpecification, secondSpecification)
        {
        }
        
        public AndSpecification(
            IResultedSpecification<T> firstSpecification, 
            IResultedSpecification<T> secondSpecification) : base(firstSpecification, secondSpecification)
        {
        }

        public AndSpecification(IEnumerable<ISpecification<T>> rulesToValidate) : base(rulesToValidate)
        {
        }

        public AndSpecification(IEnumerable<IResultedSpecification<T>> rulesToValidate) : base(rulesToValidate)
        {
        }
        
        public override bool IsPrimitiveSatisfiedBy(T entityToEvaluate)
        {
            if (!ChildResultSpecifications.Any()) return true;
            
            var rulesResult = 
                ChildPrimitiveSpecifications
                    .Select(rule => 
                        rule.IsPrimitiveSatisfiedBy(entityToEvaluate))
                    .ToList();

            return rulesResult.Any(expression => expression);
        }
        
        public override Result IsSatisfiedBy(T domainEntity)
        {
            if (!ChildResultSpecifications.Any()) return Result.Ok();
            
            var rulesResult = ChildResultSpecifications
                .Select(rule => rule.IsSatisfiedBy(domainEntity))
                .ToList();

            return rulesResult.All(result => result.IsSuccess) 
                ? Result.Ok() 
                : Result.Failure(rulesResult.SelectMany(x => x.Errors));
        }
    }
}