using Ivas.Entities.Base;
using Ivas.Entities.Models;
using System;

namespace Ivas.Transactions.Entities.Models
{
    public class Transaction : Entity
    {
        public Transaction()
        {
            TransactionId = new Guid();
        }

        public Guid TransactionId { get; set; }
        public DateTime? Date { get; set; }
        public string Ticker { get; set; }
        public decimal PricePerShare { get; set; }
        public decimal Units { get; set; }
        public virtual TransactionType Type { get; set; }
        public virtual User User { get; set; }
    }
}
