using FluentValidation;
using Ivas.Transactions.Core.Dtos;

namespace Ivas.Transactions.Core.Interfaces.Validators
{
    public interface ITransactionsCreateValidator : IValidator<TransactionSubmitDto>
    {
    }
}
