using FluentValidation;
using Ivas.Persistency.UnitOfWork.Interfaces;
using Ivas.Transactions.Core.Dtos;
using Ivas.Transactions.Core.Interfaces.Validators;
using System;

namespace Ivas.Transactions.Core.Validators
{
    public class TransactionsCreateValidator : AbstractValidator<TransactionSubmitDto>, ITransactionsCreateValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsCreateValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            RuleFor(p => p.Ticker).NotEmpty().WithMessage("Ticker cannot be empty.");

            RuleFor(p => p.PricePerShare).NotNull()
                                         .Must(value => value > (decimal)0.00)
                                         .WithMessage("The share price has to be a positive number.");

            RuleFor(p => p.Units).NotNull()
                                 .Must(value => value > (decimal)0.00)
                                 .WithMessage("The unit has to be a positive number.");

            RuleFor(p => p.Type).NotNull()
                                .WithMessage("The transaction must have a type.");
        }
    }
}
