using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Objects.Base;

namespace Babylon.Investments.Domain.Contracts.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(string clientIdentifier, string transactionId);

        Task<IEnumerable<Transaction>> GetByClientAsync(string clientIdentifier);
        
        Task Insert(Transaction transaction);

        Task InsertInBulk(IEnumerable<Transaction> InvestmentsToInsert);

        Task Delete(Transaction transaction);

        Task DeleteInBulk(IEnumerable<Transaction> InvestmentsToDelete);
    }
}