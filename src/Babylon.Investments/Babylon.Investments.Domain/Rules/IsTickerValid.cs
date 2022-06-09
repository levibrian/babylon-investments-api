using Babylon.Investments.Domain.Contracts.Enums;
using Babylon.Investments.Domain.Contracts.Requests;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsTickerValid : IResultedSpecification<TransactionPostRequest>
    {
        public Result IsSatisfiedBy(TransactionPostRequest entityToEvaluate)
        {
            var expression = entityToEvaluate.Ticker.Length <= 4;
            
            return !expression 
                ? Result.Failure(ErrorCodesEnum.TickerNotValid) 
                : Result.Ok();
        }
    }
}