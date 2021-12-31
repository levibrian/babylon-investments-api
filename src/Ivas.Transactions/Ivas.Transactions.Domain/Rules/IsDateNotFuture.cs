using System;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Domain.Requests;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
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