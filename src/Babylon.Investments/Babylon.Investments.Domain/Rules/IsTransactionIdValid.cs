using Babylon.Investments.Domain.Contracts.Dtos;
using Babylon.Investments.Domain.Contracts.Enums;
using Babylon.Investments.Domain.Rules.Primitives;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsTransactionIdValid : IResultedSpecification<TransactionPostDto>
    {
        public Result IsSatisfiedBy(TransactionPostDto entityToEvaluate)
        {
            var isTransactionIdValid = new IsGuidValid().IsPrimitiveSatisfiedBy(entityToEvaluate.TransactionId);

            return !isTransactionIdValid 
                ? Result.Failure(ErrorCodesEnum.TransactionIdProvidedNotValid) 
                : Result.Ok();
        }
    }
}