using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Brokers;

namespace Babylon.Networking.Interfaces.Brokers
{
    public interface IFinancialsBroker
    {
        Task<IEnumerable<FinancialsYearly>> GetByTicker(string ticker);
    }
}
