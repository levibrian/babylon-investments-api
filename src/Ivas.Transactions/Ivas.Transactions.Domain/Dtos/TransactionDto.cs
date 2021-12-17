using System;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Domain.Enums;

namespace Ivas.Transactions.Domain.Dtos
{
    public class TransactionDto : Dto
    {
        public string TransactionId { get; set; }

        public string UserId { get; set; }
        
        public string Ticker { get; set; }
        
        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public TransactionTypeEnum TransactionType { get; set; }
    }
}