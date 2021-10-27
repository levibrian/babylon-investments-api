using System.Collections.Generic;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Shared.Abstractions.Specifications.Base
{
    public abstract class CompositeSpecification<TCandidate> : ISpecification<TCandidate>
    {
        protected readonly List<ISpecification<TCandidate>> ChildSpecifications = new List<ISpecification<TCandidate>>();

        protected CompositeSpecification(
            ISpecification<TCandidate> firstSpecification, 
            ISpecification<TCandidate> secondSpecification)
        {
            ChildSpecifications.Add(firstSpecification);
            ChildSpecifications.Add(secondSpecification);
        }

        protected CompositeSpecification(IEnumerable<ISpecification<TCandidate>> rulesToValidate)
        {
            ChildSpecifications.AddRange(rulesToValidate);
        }

        public void AddRule(ISpecification<TCandidate> childSpecification)
        {
            ChildSpecifications.Add(childSpecification);
        }

        public void AddRuleRange(ICollection<ISpecification<TCandidate>> rulesToAdd)
        {
            ChildSpecifications.AddRange(rulesToAdd);
        }

        public abstract bool IsSatisfiedBy(TCandidate candidate);

        public IReadOnlyCollection<ISpecification<TCandidate>> Children => ChildSpecifications.AsReadOnly();
    }
}