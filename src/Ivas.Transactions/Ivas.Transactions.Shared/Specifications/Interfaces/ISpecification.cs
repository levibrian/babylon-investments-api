namespace Ivas.Transactions.Shared.Specifications.Interfaces
{
    public interface ISpecification<in T>
    {
        bool IsPrimitiveSatisfiedBy(T entityToEvaluate);
    }
}