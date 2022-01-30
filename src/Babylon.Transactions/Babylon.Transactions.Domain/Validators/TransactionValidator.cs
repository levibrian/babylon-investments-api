using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Objects;
using Babylon.Transactions.Domain.Requests;
using Babylon.Transactions.Domain.Rules;
using Babylon.Transactions.Domain.Rules.Primitives;
using Babylon.Transactions.Shared.Notifications;
using Babylon.Transactions.Shared.Validators;
using Babylon.Transactions.Shared.Extensions;

namespace Babylon.Transactions.Domain.Validators
{
    public interface ITransactionValidator : IValidator<TransactionPostDto>
    {
        Result ValidateDelete(TransactionDeleteDto objectToValidate);

        IEnumerable<Result> ValidateDelete(IEnumerable<TransactionDeleteDto> objectToValidate);

        IEnumerable<Result> Validate(IEnumerable<TransactionPostDto> objectsToValidate);
    }

    public class TransactionValidator : ITransactionValidator
    {
        public Result Validate(TransactionPostDto objectToValidate)
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

        public IEnumerable<Result> ValidateDelete(IEnumerable<TransactionDeleteDto> objectToValidate) =>
            objectToValidate.Select(ValidateDelete);

        public IEnumerable<Result> Validate(IEnumerable<TransactionPostDto> objectsToValidate) => objectsToValidate.Select(Validate);

        public Result ValidateDelete(TransactionDeleteDto objectToValidate)
        {
            var transactionToValidate = new TransactionPostDto()
            {
                ClientIdentifier = objectToValidate.ClientIdentifier,
                TransactionId = objectToValidate.TransactionId
            };
            
            var transactionIdRules = 
                new IsClientIdentifierProvided()
                    .And(new IsTransactionIdValid());

            return transactionIdRules.IsSatisfiedBy(transactionToValidate);
        }
    }
}