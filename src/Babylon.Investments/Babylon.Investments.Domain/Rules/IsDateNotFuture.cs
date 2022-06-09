using System;
using Babylon.Investments.Domain.Contracts.Enums;
using Babylon.Investments.Domain.Contracts.Requests;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsDateNotFuture : IResultedSpecification<TransactionPostRequest>
    {
        public Result IsSatisfiedBy(TransactionPostRequest entityToEvaluate)
        {
            var expression = entityToEvaluate.Date.Date == DateTime.UtcNow.Date || 
                             entityToEvaluate.Date.Date < DateTime.UtcNow.Date;

            return !expression 
                ? Result.Failure(ErrorCodesEnum.DateIsFutureDate) 
                : Result.Ok();
        }
    }
}