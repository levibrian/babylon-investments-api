using System.Collections.Generic;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Networking;

namespace Ivas.Transactions.Networking.Interfaces.Brokers
{
    public interface IFinancialsBroker
    {
        Task<IEnumerable<FinancialsYearly>> GetByTicker(string ticker);
    }
}
