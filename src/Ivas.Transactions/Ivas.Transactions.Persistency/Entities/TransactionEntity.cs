using System;
using Ivas.Transactions.Persistency.Abstractions.Entities;

namespace Ivas.Transactions.Persistency.Entities
{
    public class TransactionEntity : Entity
    {
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public virtual TransactionTypeEntity TransactionType { get; set; }
    }
}