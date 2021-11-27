namespace Ivas.Transactions.Shared.Specifications.Interfaces
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T entityToEvaluate);
    }
}