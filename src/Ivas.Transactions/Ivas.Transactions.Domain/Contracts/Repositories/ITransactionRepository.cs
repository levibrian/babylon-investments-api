using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Contracts.Repositories.Base;
using Ivas.Transactions.Domain.Objects;

namespace Ivas.Transactions.Domain.Contracts.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(string userId, string transactionId);

        Task<IEnumerable<Transaction>> GetByUserAsync(string userId);
        
        Task Insert(Transaction transaction);

        Task InsertInBulk(IEnumerable<Transaction> transactionsToInsert);

        Task Delete(Transaction transaction);
    }
}