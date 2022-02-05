using System;
using Babylon.Investments.Domain.Dtos;
using Babylon.Investments.Domain.Enums;
using Babylon.Investments.Domain.Requests;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsDateNotFuture : IResultedSpecification<TransactionPostDto>
    {
        public Result IsSatisfiedBy(TransactionPostDto entityToEvaluate)
        {
            var expression = entityToEvaluate.Date.Date == DateTime.UtcNow.Date || 
                             entityToEvaluate.Date.Date < DateTime.UtcNow.Date;

            return !expression 
                ? Result.Failure(ErrorCodesEnum.DateIsFutureDate) 
                : Result.Ok();
        }
    }
}