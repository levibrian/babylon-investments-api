using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsUserIdPositive : IResultedSpecification<TransactionDto>
    {
        public Result IsSatisfiedBy(TransactionDto entityToEvaluate)
        {
            var expression = entityToEvaluate.UserId > 0;
            
            return !expression 
                ? Result.Failure(ErrorCodesEnum.UserIdNotPositive) 
                : Result.Ok();
        }
    }
}