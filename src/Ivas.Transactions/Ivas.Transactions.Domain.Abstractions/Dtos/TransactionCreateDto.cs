using System;
using Ivas.Transactions.Domain.Abstractions.Dtos.Base;
using Ivas.Transactions.Domain.Abstractions.Enums;

namespace Ivas.Transactions.Domain.Abstractions.Dtos
{
    public class TransactionCreateDto : Dto
    {
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public virtual TransactionTypeEnum TransactionType { get; set; }
    }
}