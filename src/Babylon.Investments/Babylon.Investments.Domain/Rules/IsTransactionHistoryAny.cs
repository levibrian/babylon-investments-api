using System.Collections.Generic;
using System.Linq;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class IsTransactionHistoryAny : IResultedSpecification<ICollection<Transaction>>
    {
        public Result IsSatisfiedBy(ICollection<Transaction> transactionHistory)
        {
            var expression = transactionHistory != null && transactionHistory.Any();
            
            return expression 
                ? Result.Ok() 
                : Result.Failure(ErrorCodesEnum.TransactionHistoryNonExistent);
        }
    }
}