using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class AreUnitsPositive : IResultedSpecification<TransactionPostRequest>
    {
        public Result IsSatisfiedBy(TransactionPostRequest entityToEvaluate)
        {
            var expression = entityToEvaluate.Units > 0;

            return !expression 
                ? Result.Failure(ErrorCodesEnum.UnitsNotPositive) 
                : Result.Ok();
        }
    }
}