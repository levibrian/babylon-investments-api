using System;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;

namespace Ivas.Transactions.Domain.Objects
{
    public class Transaction : Abstractions.Objects.Domain
    {
        public virtual string TransactionId { get; set; }

        public virtual long UserId { get; set; }
        
        public virtual string Ticker { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual decimal Units { get; set; }

        public virtual decimal PricePerUnit { get; set; }
        
        public virtual TransactionTypeEnum TransactionType { get; set; }

        protected Transaction()
        {
        }

        public Transaction(TransactionDto dto)
        {
            TransactionId = dto.TransactionId;
            UserId = dto.UserId;
        }
    }
}