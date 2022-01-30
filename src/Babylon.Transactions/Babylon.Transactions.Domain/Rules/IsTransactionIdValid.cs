using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Enums;
using Babylon.Transactions.Domain.Rules.Primitives;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Domain.Rules
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