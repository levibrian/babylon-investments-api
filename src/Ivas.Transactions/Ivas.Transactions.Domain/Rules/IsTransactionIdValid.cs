using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsTransactionIdValid : IResultedSpecification<TransactionDto>
    {
        public Result IsSatisfiedBy(TransactionDto entityToEvaluate)
        {
            var isUserIdValid = new IsGuidValid().IsSatisfiedBy(entityToEvaluate.TransactionId);

            var expression = isUserIdValid.IsSuccess;
            
            return !expression 
                ? Result.Failure(ErrorCodesEnum.TransactionIdProvidedNotValid) 
                : Result.Ok();
        }
    }
}