using System;
using System.Collections.Generic;
using System.Linq;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Base;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Shared.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(
            IResultedSpecification<T> firstSpecification, 
            IResultedSpecification<T> secondSpecification) : base(firstSpecification, secondSpecification)
        {
        }

        public OrSpecification(IEnumerable<IResultedSpecification<T>> rulesToValidate) : base(rulesToValidate)
        {
        }
        
        public override Result IsSatisfiedBy(T domainEntity)
        {
            if (!ChildSpecifications.Any()) return Result.Ok();
            
            var rulesResult = 
                ChildSpecifications
                    .Select(rule => 
                        rule.IsSatisfiedBy(domainEntity))
                    .ToList();

            return rulesResult.Any(result => result.IsSuccess)
                ? Result.Ok()
                : Result.Failure(rulesResult.SelectMany(x => x.Errors));
        }
    }
}