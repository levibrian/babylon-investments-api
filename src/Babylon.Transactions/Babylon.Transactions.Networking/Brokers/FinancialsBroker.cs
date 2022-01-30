using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Transactions.Domain.Abstractions.Networking;
using Babylon.Transactions.Networking.Base;
using Babylon.Transactions.Networking.Constants;
using Babylon.Transactions.Networking.Enums;
using Babylon.Transactions.Networking.Interfaces.Brokers;

namespace Babylon.Transactions.Networking.Brokers
{
    public class FinancialsBroker : PolygonBroker, IFinancialsBroker
    {
        public async Task<IEnumerable<FinancialsYearly>> GetByTicker(string ticker)
        {
            return await Get<FinancialsYearly>(PolygonApiRoutes.GetFinancialsApiRouteByType(ticker, FinancialsTypes.Y));
        }
    }
}
