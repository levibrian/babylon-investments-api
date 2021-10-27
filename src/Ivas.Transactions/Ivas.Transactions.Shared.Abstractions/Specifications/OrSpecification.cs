using System;
using System.Collections.Generic;
using System.Linq;
using Ivas.Transactions.Shared.Abstractions.Specifications.Base;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Shared.Abstractions.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(
            ISpecification<T> firstSpecification, 
            ISpecification<T> secondSpecification) : base(firstSpecification, secondSpecification)
        {
        }

        public OrSpecification(IEnumerable<ISpecification<T>> rulesToValidate) : base(rulesToValidate)
        {
        }
        
        public override bool IsSatisfiedBy(T domainEntity)
        {
            if (!ChildSpecifications.Any()) return false;
            
            var rulesResult = new List<bool>();
            
            foreach (var rule in ChildSpecifications)
            {
                rulesResult.Add(rule.IsSatisfiedBy(domainEntity));
            }

            return rulesResult.Any(result => result);
        }
    }
}