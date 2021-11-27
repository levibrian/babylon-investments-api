using Ivas.Transactions.Shared.Notifications;

namespace Ivas.Transactions.Shared.Specifications.Interfaces
{
    public interface IResultedSpecification<in T>
    {
        Result IsSatisfiedBy(T entityToEvaluate);
    }
}