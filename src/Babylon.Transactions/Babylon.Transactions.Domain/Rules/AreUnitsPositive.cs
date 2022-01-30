using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Enums;
using Babylon.Transactions.Domain.Requests;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Specifications.Interfaces;

namespace Babylon.Transactions.Domain.Rules
{
    public class AreUnitsPositive : IResultedSpecification<TransactionPostDto>
    {
        public Result IsSatisfiedBy(TransactionPostDto entityToEvaluate)
        {
            var expression = entityToEvaluate.Units > 0;

            return !expression 
                ? Result.Failure(ErrorCodesEnum.UnitsNotPositive) 
                : Result.Ok();
        }
    }
}