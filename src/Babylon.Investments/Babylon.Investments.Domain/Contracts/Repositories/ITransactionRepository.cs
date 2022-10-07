using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Objects.Base;

namespace Babylon.Investments.Domain.Contracts.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(string tenantId, string transactionId);

        Task<IEnumerable<Transaction>> GetByTenantAsync(string tenantId);
        
        Task Insert(Transaction transaction);

        Task Delete(Transaction transaction);

        Task DeleteInBulk(IEnumerable<Transaction> investmentsToDelete);
    }
}