using Babylon.Investments.Domain.Contracts.Dtos;
using Babylon.Investments.Domain.Contracts.Enums;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class AreUnitsPositive : IResultedSpecification<TransactionPostDto>
    {
        public Result IsSatisfiedBy(TransactionPostDto entityToEvaluate)
        {
            var expression = entityToEvaluate.Units > 0;

            return !expression 
                ? Result.Failure(ErrorCodesEnum.UnitsNotPositive) 
                : Result.Ok();
        }
    }
}