using Babylon.Investments.Shared.Notifications;

namespace Babylon.Investments.Shared.Specifications.Interfaces
{
    public interface IResultedSpecification<in T>
    {
        Result IsSatisfiedBy(T entityToEvaluate);
    }
}