using Babylon.Investments.Domain.Dtos;
using Babylon.Investments.Domain.Enums;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsClientIdentifierProvided : IResultedSpecification<TransactionPostDto>
    {
        public Result IsSatisfiedBy(TransactionPostDto transaction)
        {
            var expression =
                !string.IsNullOrEmpty(transaction.ClientIdentifier);
            
            return expression 
                ? Result.Ok() 
                : Result.Failure(Error.CreateError(ErrorCodesEnum.ClientIdentifierNotProvided));
        }
    }
}