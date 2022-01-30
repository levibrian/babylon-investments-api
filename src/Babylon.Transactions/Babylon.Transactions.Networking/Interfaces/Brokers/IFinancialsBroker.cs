﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Transactions.Domain.Abstractions.Networking;

namespace Babylon.Transactions.Networking.Interfaces.Brokers
{
    public interface IFinancialsBroker
    {
        Task<IEnumerable<FinancialsYearly>> GetByTicker(string ticker);
    }
}