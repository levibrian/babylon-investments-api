namespace Babylon.Investments.Shared.Specifications.Interfaces
{
    public interface ISpecification<in T>
    {
        bool IsPrimitiveSatisfiedBy(T entityToEvaluate);
    }
}