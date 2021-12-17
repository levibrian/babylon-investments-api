using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class AreUnitsPositive : IResultedSpecification<TransactionSubmitDto>
    {
        public Result IsSatisfiedBy(TransactionSubmitDto entityToEvaluate)
        {
            var expression = entityToEvaluate.Units > 0;

            return !expression 
                ? Result.Failure(ErrorCodesEnum.UnitsNotPositive) 
                : Result.Ok();
        }
    }
}