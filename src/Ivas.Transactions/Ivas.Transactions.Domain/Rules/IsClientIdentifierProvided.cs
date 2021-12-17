using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsClientIdentifierProvided : IResultedSpecification<TransactionSubmitDto>
    {
        public Result IsSatisfiedBy(TransactionSubmitDto entityToEvaluate)
        {
            var expression =
                !string.IsNullOrEmpty(entityToEvaluate.ClientIdentifier);
            
            return !expression 
                ? Result.Failure(ErrorCodesEnum.ClientIdentifierNotProvided) 
                : Result.Ok();
        }
    }
}