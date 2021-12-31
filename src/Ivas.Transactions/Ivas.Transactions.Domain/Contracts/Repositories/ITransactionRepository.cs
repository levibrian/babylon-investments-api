using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Contracts.Repositories.Base;
using Ivas.Transactions.Domain.Objects;

namespace Ivas.Transactions.Domain.Contracts.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(string clientIdentifier, string transactionId);

        Task<IEnumerable<Transaction>> GetByClientAsync(string clientIdentifier);
        
        Task Insert(Transaction transaction);

        Task InsertInBulk(IEnumerable<Transaction> transactionsToInsert);

        Task Delete(Transaction transaction);

        Task DeleteInBulk(IEnumerable<Transaction> transactionsToDelete);
    }
}