using Babylon.Investments.Domain.Contracts.Enums;
using Babylon.Investments.Domain.Contracts.Requests;
using Babylon.Investments.Domain.Rules.Primitives;
using Babylon.Investments.Shared.Extensions;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsUserIdValid : IResultedSpecification<TransactionPostRequest>
    {
        public Result IsSatisfiedBy(TransactionPostRequest entityToEvaluate)
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