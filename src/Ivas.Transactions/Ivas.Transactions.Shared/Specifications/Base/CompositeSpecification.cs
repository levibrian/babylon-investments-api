using System.Collections.Generic;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Shared.Specifications.Base
{
    public abstract class CompositeSpecification<TCandidate> : IResultedSpecification<TCandidate>
    {
        protected readonly List<IResultedSpecification<TCandidate>> ChildSpecifications = new List<IResultedSpecification<TCandidate>>();

        protected CompositeSpecification(
            IResultedSpecification<TCandidate> firstSpecification, 
            IResultedSpecification<TCandidate> secondSpecification)
        {
            ChildSpecifications.Add(firstSpecification);
            ChildSpecifications.Add(secondSpecification);
        }

        protected CompositeSpecification(IEnumerable<IResultedSpecification<TCandidate>> rulesToValidate)
        {
            ChildSpecifications.AddRange(rulesToValidate);
        }

        public void AddRule(IResultedSpecification<TCandidate> childSpecification)
        {
            ChildSpecifications.Add(childSpecification);
        }

        public void AddRuleRange(ICollection<IResultedSpecification<TCandidate>> rulesToAdd)
        {
            ChildSpecifications.AddRange(rulesToAdd);
        }

        public abstract Result IsSatisfiedBy(TCandidate candidate);

        public IReadOnlyCollection<IResultedSpecification<TCandidate>> Children => ChildSpecifications.AsReadOnly();
    }
}