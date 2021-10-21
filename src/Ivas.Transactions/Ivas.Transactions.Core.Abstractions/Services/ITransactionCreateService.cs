using Ivas.Transactions.Core.Abstractions.Services.Base.Interfaces;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Persistency.Entities;

namespace Ivas.Transactions.Core.Abstractions.Services
{
    public interface ITransactionCreateService : IAsyncService<TransactionEntity, TransactionCreateDto>
    {
        
    }
}