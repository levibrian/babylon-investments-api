using System.Collections.Generic;
using System.Linq;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsTransactionHistoryAny : IResultedSpecification<IEnumerable<Transaction>>
    {
        public Result IsSatisfiedBy(IEnumerable<Transaction> entityToEvaluate)
        {
            var expression = entityToEvaluate != null && entityToEvaluate.Any();
            
            return !expression 
                ? Result.Failure(ErrorCodesEnum.TickerNotValid) 
                : Result.Ok();
        }
    }
}