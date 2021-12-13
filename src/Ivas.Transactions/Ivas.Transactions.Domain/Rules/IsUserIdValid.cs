using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Shared.Extensions;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsUserIdValid : IResultedSpecification<TransactionDto>
    {
        public Result IsSatisfiedBy(TransactionDto entityToEvaluate)
        {
            var isUserIdValid = 
                new IsStringNumber()
                    .Or(new IsGuidValid())
                    .IsSatisfiedBy(entityToEvaluate.UserId);

            var expression = isUserIdValid.IsSuccess;
            
            return !expression 
                ? Result.Failure(ErrorCodesEnum.UserIdProvidedNotValid) 
                : Result.Ok();
        }
    }
}