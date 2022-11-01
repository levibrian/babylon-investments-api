using System.Collections.Generic;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Shared.Notifications;

namespace Babylon.Investments.Domain.Validators
{
    public interface IOperationValidator
    {
        Result Validate(IEnumerable<Transaction> transactionHistory);
    }
    
    public class OperationValidator : IOperationValidator
    {
        public Result Validate(IEnumerable<Transaction> transactionHistory)
        {
            throw new System.NotImplementedException();
        }
    }
}