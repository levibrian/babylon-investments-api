using Ivas.Core.Dtos.Base;
using Ivas.Transactions.Core.Enums;
using System;

namespace Ivas.Transactions.Core.Dtos
{
    public class TransactionDto : Dto
    {
        public Guid TransactionId { get; set; }
        public DateTime? Date { get; set; }
        public string Ticker { get; set; }
        public decimal PricePerShare { get; set; }
        public decimal Units { get; set; }
        public virtual TransactionTypeEnum Type { get; set; }
    }
}
