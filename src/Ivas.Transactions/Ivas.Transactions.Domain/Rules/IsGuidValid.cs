using System;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsGuidValid : IResultedSpecification<TransactionDto>
    {
        public Result IsSatisfiedBy(TransactionDto entityToEvaluate)
        {
            var expression = Guid.TryParse(entityToEvaluate.TransactionId, out var guidOutput);

            return !expression
                ? Result.Failure(ErrorCodesEnum.GuidProvidedNotValid)
                : Result.Ok();
        }
    }
}