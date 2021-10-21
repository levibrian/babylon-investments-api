using System;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Shared.Abstractions.Specifications
{
    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _firstSpecification;
        private readonly ISpecification<T> _secondSpecification;

        public AndSpecification(ISpecification<T> firstSpecification, ISpecification<T> secondSpecification)
        {
            _firstSpecification = firstSpecification ?? throw new ArgumentNullException(nameof(firstSpecification));
            _secondSpecification = secondSpecification ?? throw new ArgumentNullException(nameof(secondSpecification));
        }
        
        public bool IsSatisfiedBy(T domainEntity)
        {
            return _firstSpecification.IsSatisfiedBy(domainEntity) && _secondSpecification.IsSatisfiedBy(domainEntity);
        }
    }
}