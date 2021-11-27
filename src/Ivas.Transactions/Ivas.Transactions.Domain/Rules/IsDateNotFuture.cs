using System;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsDateNotFuture : IResultedSpecification<TransactionCreateDto>
    {
        public Result IsSatisfiedBy(TransactionCreateDto entityToEvaluate)
        {
            var expression = entityToEvaluate.Date.Date == DateTime.UtcNow.Date || 
                             entityToEvaluate.Date.Date < DateTime.UtcNow.Date;

            return !expression 
                ? Result.Failure(ErrorCodesEnum.DateIsFutureDate) 
                : Result.Ok();
        }
    }
}