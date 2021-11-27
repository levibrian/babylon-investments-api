using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Domain.Rules;
using Ivas.Transactions.Shared.Notifications;
using Ivas.Transactions.Shared.Validators;
using Ivas.Transactions.Shared.Extensions;

namespace Ivas.Transactions.Domain.Validators
{
    public interface ITransactionValidator : IValidator<TransactionDto>
    {
        Result Validate(TransactionCreateDto objectToValidate);
    }

    public class TransactionValidator : ITransactionValidator
    {
        public Result Validate(TransactionCreateDto objectToValidate)
        {
            var transactionRules = 
                new IsTickerProvided()
                .And(new IsTickerValid())
                .And(new IsUserIdPositive())
                .And(new IsDateNotFuture())
                .And(new AreUnitsPositive())
                .And(new IsPricePositive());

            return transactionRules.IsSatisfiedBy(objectToValidate);
        }

        public Result Validate(TransactionDto objectToValidate)
        {
            var validationRules =
                new IsGuidValid()
                .And(new IsUserIdPositive());

            return validationRules.IsSatisfiedBy(objectToValidate);
        }
    }
}