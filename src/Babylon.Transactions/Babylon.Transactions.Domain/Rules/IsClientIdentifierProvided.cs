using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Enums;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Domain.Rules
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