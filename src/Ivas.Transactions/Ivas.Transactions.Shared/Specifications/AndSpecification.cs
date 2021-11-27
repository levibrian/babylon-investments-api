using System;
using System.Collections.Generic;
using System.Linq;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Base;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Shared.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(
            IResultedSpecification<T> firstSpecification, 
            IResultedSpecification<T> secondSpecification) : base(firstSpecification, secondSpecification)
        {
        }

        public AndSpecification(IEnumerable<IResultedSpecification<T>> rulesToValidate) : base(rulesToValidate)
        {
        }
        
        public override Result IsSatisfiedBy(T domainEntity)
        {
            if (!ChildSpecifications.Any()) return Result.Ok();
            
            var rulesResult = new List<Result>();
            
            foreach (var rule in ChildSpecifications)
            {
                rulesResult.Add(rule.IsSatisfiedBy(domainEntity));
            }

            return rulesResult.All(result => result.IsSuccess) 
                ? Result.Ok() : 
                Result.Failure(rulesResult.SelectMany(x => x.Errors));
        }
    }
}