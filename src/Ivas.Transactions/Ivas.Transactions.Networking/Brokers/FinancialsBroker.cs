using System.Collections.Generic;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Networking;
using Ivas.Transactions.Networking.Base;
using Ivas.Transactions.Networking.Constants;
using Ivas.Transactions.Networking.Enums;
using Ivas.Transactions.Networking.Interfaces.Brokers;

namespace Ivas.Transactions.Networking.Brokers
{
    public class FinancialsBroker : PolygonBroker, IFinancialsBroker
    {
        public async Task<IEnumerable<FinancialsYearly>> GetByTicker(string ticker)
        {
            return await Get<FinancialsYearly>(PolygonApiRoutes.GetFinancialsApiRouteByType(ticker, FinancialsTypes.Y));
        }
    }
}
