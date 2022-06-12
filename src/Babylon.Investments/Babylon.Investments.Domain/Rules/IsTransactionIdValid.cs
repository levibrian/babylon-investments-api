using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Rules.Primitives;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsTransactionIdValid : IResultedSpecification<TransactionBaseRequest>
    {
        public Result IsSatisfiedBy(TransactionBaseRequest entityToEvaluate)
        {
            var isTransactionIdValid = new IsGuidValid().IsPrimitiveSatisfiedBy(entityToEvaluate.TransactionId);

            return !isTransactionIdValid 
                ? Result.Failure(ErrorCodesEnum.TransactionIdProvidedNotValid) 
                : Result.Ok();
        }
    }
}