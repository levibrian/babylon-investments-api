using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Networking.Base;
using Babylon.Networking.Constants;
using Babylon.Networking.Enums;
using Babylon.Networking.Interfaces.Brokers;
using Babylon.Transactions.Domain.Abstractions.Networking;

namespace Babylon.Networking.Brokers
{
    public class FinancialsBroker : PolygonBroker, IFinancialsBroker
    {
        public async Task<IEnumerable<FinancialsYearly>> GetByTicker(string ticker)
        {
            return await Get<FinancialsYearly>(PolygonApiRoutes.GetFinancialsApiRouteByType(ticker, FinancialsTypes.Y));
        }
    }
}
