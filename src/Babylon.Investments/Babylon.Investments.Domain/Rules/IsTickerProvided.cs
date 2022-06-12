using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsTickerProvided : IResultedSpecification<TransactionPostRequest>
    {
        public Result IsSatisfiedBy(TransactionPostRequest entityToEvaluate)
        {
            var expression = entityToEvaluate.Ticker != null && 
                             !string.IsNullOrEmpty(entityToEvaluate.Ticker) &&
                             !string.IsNullOrWhiteSpace(entityToEvaluate.Ticker);

            return !expression 
                ? Result.Failure(ErrorCodesEnum.TickerNotProvided) 
                : Result.Ok();
        }
    }
}