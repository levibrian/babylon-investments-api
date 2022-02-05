using System.Collections.Generic;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Shared.Specifications.Base
{
    public abstract class CompositeSpecification<TCandidate> : IResultedSpecification<TCandidate>, ISpecification<TCandidate>
    {
        protected readonly List<ISpecification<TCandidate>> ChildPrimitiveSpecifications =
            new List<ISpecification<TCandidate>>();
        
        protected readonly List<IResultedSpecification<TCandidate>> ChildResultSpecifications =
            new List<IResultedSpecification<TCandidate>>();

        protected CompositeSpecification(
            ISpecification<TCandidate> firstSpecification, 
            ISpecification<TCandidate> secondSpecification)
        {
            ChildPrimitiveSpecifications.Add(firstSpecification);
            ChildPrimitiveSpecifications.Add(secondSpecification);
        }
        
        protected CompositeSpecification(
            IResultedSpecification<TCandidate> firstSpecification, 
            IResultedSpecification<TCandidate> secondSpecification)
        {
            ChildResultSpecifications.Add(firstSpecification);
            ChildResultSpecifications.Add(secondSpecification);
        }
        
        protected CompositeSpecification(IEnumerable<ISpecification<TCandidate>> rulesToValidate) => ChildPrimitiveSpecifications.AddRange(rulesToValidate);
        
        protected CompositeSpecification(IEnumerable<IResultedSpecification<TCandidate>> rulesToValidate) => ChildResultSpecifications.AddRange(rulesToValidate);

        public void AddRule(ISpecification<TCandidate> childSpecification) => ChildPrimitiveSpecifications.Add(childSpecification);
        
        public void AddRule(IResultedSpecification<TCandidate> childSpecification) => ChildResultSpecifications.Add(childSpecification);

        public void AddRuleRange(ICollection<ISpecification<TCandidate>> rulesToAdd) => ChildPrimitiveSpecifications.AddRange(rulesToAdd);
        
        public void AddRuleRange(ICollection<IResultedSpecification<TCandidate>> rulesToAdd) => ChildResultSpecifications.AddRange(rulesToAdd);
        
        public abstract Result IsSatisfiedBy(TCandidate candidate);

        public abstract bool IsPrimitiveSatisfiedBy(TCandidate entityToEvaluate);

        public IReadOnlyCollection<IResultedSpecification<TCandidate>> Children => ChildResultSpecifications.AsReadOnly();
    }
}