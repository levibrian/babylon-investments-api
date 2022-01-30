using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Enums;
using Babylon.Transactions.Domain.Rules.Primitives;
using Babylon.Transactions.Shared.Extensions;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Domain.Rules
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