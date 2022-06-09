using System.Collections.Generic;
using System.Linq;
using Babylon.Investments.Domain.Contracts.Requests;
using Babylon.Investments.Domain.Rules;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Validators;
using Babylon.Investments.Shared.Extensions;

namespace Babylon.Investments.Domain.Validators
{
    public interface ITransactionValidator : IValidator<TransactionPostRequest>
    {
        Result ValidateDelete(TransactionDeleteRequest objectToValidate);

        IEnumerable<Result> ValidateDelete(IEnumerable<TransactionDeleteRequest> objectToValidate);

        IEnumerable<Result> Validate(IEnumerable<TransactionPostRequest> objectsToValidate);
    }

    public class TransactionValidator : ITransactionValidator
    {
        public Result Validate(TransactionPostRequest objectToValidate)
        {
            var transactionRules = 
                new IsTickerProvided()
                    .And(new IsTickerValid())
                    .And(new IsUserIdValid())
                    .And(new IsClientIdentifierProvided())
                    .And(new IsDateNotFuture())
                    .And(new AreUnitsPositive())
                    .And(new IsPricePositive());

            return transactionRules.IsSatisfiedBy(objectToValidate);
        }

        public IEnumerable<Result> ValidateDelete(IEnumerable<TransactionDeleteRequest> objectToValidate) =>
            objectToValidate.Select(ValidateDelete);

        public IEnumerable<Result> Validate(IEnumerable<TransactionPostRequest> objectsToValidate) => objectsToValidate.Select(Validate);

        public Result ValidateDelete(TransactionDeleteRequest objectToValidate)
        {
            var transactionIdRules = 
                new IsClientIdentifierProvided()
                    .And(new IsTransactionIdValid());

            return transactionIdRules.IsSatisfiedBy(objectToValidate);
        }
    }
}