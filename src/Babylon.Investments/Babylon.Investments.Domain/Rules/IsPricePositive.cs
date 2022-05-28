using Babylon.Investments.Domain.Abstractions.Dtos;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsPricePositive : IResultedSpecification<TransactionPostDto>
    {
        public Result IsSatisfiedBy(TransactionPostDto entityToEvaluate)
        {
            var expression = entityToEvaluate.PricePerUnit > 0;

            return !expression 
                ? Result.Failure(ErrorCodesEnum.PriceNotPositive) 
                : Result.Ok();
        }
    }
}