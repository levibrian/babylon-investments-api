using Babylon.Investments.Domain.Dtos;
using Babylon.Investments.Domain.Enums;
using Babylon.Investments.Domain.Rules.Primitives;
using Babylon.Investments.Shared.Extensions;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
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