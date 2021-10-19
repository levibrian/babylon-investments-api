using Ivas.Core.Services.Interfaces;
using Ivas.Transactions.Core.Dtos;
using Ivas.Transactions.Entities.Models;

namespace Ivas.Transactions.Core.Interfaces.Services
{
    public interface ITransactionsService : ICreatableAsyncService<Transaction, TransactionSubmitDto>, IReadOnlyAsyncService<Transaction, TransactionDto>
    {
    }
}
