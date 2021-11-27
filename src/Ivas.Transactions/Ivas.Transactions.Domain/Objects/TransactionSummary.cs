using System;
using System.Collections.Generic;
using System.Linq;

namespace Ivas.Transactions.Domain.Objects
{
    public class TransactionSummary : Transaction
    {
        public decimal RealizedGainLoss { get; set; }
        
        public decimal RealizedDividends { get; set; }

        public decimal TotalInvested { get; set; }
        
        private readonly IEnumerable<Transaction> _transactions;

        public TransactionSummary()
        {
        }

        public TransactionSummary(IEnumerable<Transaction> transactions)
        {
            _transactions = transactions 
                            ?? throw new ArgumentNullException(nameof(transactions));

            // PricePerUnit = transactions.Average(x => PricePerUnit);
            //
            // Units = transactions
        }
    }
}