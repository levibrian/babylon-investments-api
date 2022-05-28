using Babylon.Investments.Domain.Abstractions.Dtos;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsTickerProvided : IResultedSpecification<TransactionPostDto>
    {
        public Result IsSatisfiedBy(TransactionPostDto entityToEvaluate)
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