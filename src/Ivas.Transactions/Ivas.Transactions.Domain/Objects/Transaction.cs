using System;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;

namespace Ivas.Transactions.Domain.Objects
{
    public class Transaction : Abstractions.Objects.Domain
    {
        public string Id { get; set; }

        public long UserId { get; set; }
        
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public TransactionTypeEnum TransactionType { get; set; }

        protected Transaction()
        {
        }

        public Transaction(TransactionDto dto)
        {
            Id = dto.Id;
            UserId = dto.UserId;
        }
    }
}