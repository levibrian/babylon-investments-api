using System;
using Ivas.Transactions.Domain.Abstractions.Dtos;

namespace Ivas.Transactions.Domain.Dtos
{
    public class TransactionDto : Dto
    {
        public long UserId { get; set; }
        
        public string Ticker { get; set; }
        
        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
    }
}