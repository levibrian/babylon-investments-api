using Babylon.Investments.Domain.Contracts.Enums;
using Babylon.Investments.Domain.Contracts.Requests;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsClientIdentifierProvided : IResultedSpecification<TransactionBaseRequest>
    {
        public Result IsSatisfiedBy(TransactionBaseRequest transaction)
        {
            var expression =
                !string.IsNullOrEmpty(transaction.ClientIdentifier);
            
            return expression 
                ? Result.Ok() 
                : Result.Failure(Error.CreateError(ErrorCodesEnum.ClientIdentifierNotProvided));
        }
    }
}