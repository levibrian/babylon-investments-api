using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Domain.Rules.Primitives;
using Ivas.Transactions.Shared.Extensions;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsUserIdValid : IResultedSpecification<TransactionPostDto>
    {
        public Result IsSatisfiedBy(TransactionPostDto entityToEvaluate)
        {
            var isUserIdValid = 
                new IsStringNumberValid()
                    .Or(new IsGuidValid())
                    .IsPrimitiveSatisfiedBy(entityToEvaluate.UserId);

            return !isUserIdValid 
                ? Result.Failure(ErrorCodesEnum.UserIdProvidedNotValid) 
                : Result.Ok();
        }
    }
}