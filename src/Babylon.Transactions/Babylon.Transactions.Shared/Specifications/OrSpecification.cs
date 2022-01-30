using System;
using System.Collections.Generic;
using System.Linq;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Base;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Shared.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(
            ISpecification<T> firstSpecification, 
            ISpecification<T> secondSpecification) : base(firstSpecification, secondSpecification)
        {
        }
        
        public OrSpecification(
            IResultedSpecification<T> firstSpecification, 
            IResultedSpecification<T> secondSpecification) : base(firstSpecification, secondSpecification)
        {
        }

        public OrSpecification(IEnumerable<ISpecification<T>> rulesToValidate) : base(rulesToValidate)
        {
        }
        
        public OrSpecification(IEnumerable<IResultedSpecification<T>> rulesToValidate) : base(rulesToValidate)
        {
        }
        
        public override bool IsPrimitiveSatisfiedBy(T entityToEvaluate)
        {
            if (!ChildPrimitiveSpecifications.Any()) return true;
            
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
            
            var rulesResult = 
                ChildResultSpecifications
                    .Select(rule => 
                        rule.IsSatisfiedBy(domainEntity))
                    .ToList();

            return rulesResult.Any(result => result.IsSuccess)
                ? Result.Ok()
                : Result.Failure(rulesResult.SelectMany(x => x.Errors));
        }
    }
}