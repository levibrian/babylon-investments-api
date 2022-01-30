using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Transactions.Domain.Abstractions.Networking;

namespace Babylon.Networking.Interfaces.Brokers
{
    public interface IFinancialsBroker
    {
        Task<IEnumerable<FinancialsYearly>> GetByTicker(string ticker);
    }
}
