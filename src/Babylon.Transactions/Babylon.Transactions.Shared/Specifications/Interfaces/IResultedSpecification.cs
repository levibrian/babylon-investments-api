using Babylon.Transactions.Shared.Notifications;

namespace Babylon.Transactions.Shared.Specifications.Interfaces
{
    public interface IResultedSpecification<in T>
    {
        Result IsSatisfiedBy(T entityToEvaluate);
    }
}