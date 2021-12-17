using System.Collections.Generic;
using System.Linq;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Domain.Rules;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Validators;
using Ivas.Transactions.Shared.Extensions;

namespace Ivas.Transactions.Domain.Validators
{
    public interface ITransactionValidator : IValidator<TransactionSubmitDto>
    {
        Result ValidateDelete(TransactionSubmitDto objectToValidate);

        IEnumerable<Result> Validate(IEnumerable<TransactionSubmitDto> objectsToValidate);
    }

    public class TransactionValidator : ITransactionValidator
    {
        public Result Validate(TransactionSubmitDto objectToValidate)
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

        public IEnumerable<Result> Validate(IEnumerable<TransactionSubmitDto> objectsToValidate) => objectsToValidate.Select(Validate);

        public Result ValidateDelete(TransactionSubmitDto objectToValidate)
        {
            var validationRules =
                new IsTransactionIdValid().And(new IsClientIdentifierProvided());

            return validationRules.IsSatisfiedBy(objectToValidate);
        }
    }
}