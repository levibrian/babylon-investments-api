using System;
using Ivas.Transactions.Domain.Abstractions.Enums;

namespace Ivas.Transactions.Domain.Objects
{
    public class Transaction : Abstractions.Objects.Domain
    {
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public TransactionTypeEnum TransactionType { get; set; }
    }
}