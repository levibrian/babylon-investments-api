using System;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsStringNumber : IResultedSpecification<string>
    {
        public Result IsSatisfiedBy(string stringToEvaluate)
        {
            var expression = 
                !string.IsNullOrWhiteSpace(stringToEvaluate) && 
                int.TryParse(stringToEvaluate, out var convertedInt) &&
                convertedInt > 0;

            return !expression
                ? Result.Failure(ErrorCodesEnum.TransactionIdProvidedNotValid)
                : Result.Ok();
        }
    }
}