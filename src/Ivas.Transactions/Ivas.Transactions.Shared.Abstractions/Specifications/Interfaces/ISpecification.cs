namespace Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T entityToEvaluate);
    }
}