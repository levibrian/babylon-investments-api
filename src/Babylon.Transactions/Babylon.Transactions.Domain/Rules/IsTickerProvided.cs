using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Enums;
using Babylon.Transactions.Domain.Requests;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Domain.Rules
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