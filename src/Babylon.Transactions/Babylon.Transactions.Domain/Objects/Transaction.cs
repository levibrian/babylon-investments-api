using System;
using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Enums;

namespace Babylon.Transactions.Domain.Objects
{
    public class Transaction : Abstractions.Objects.Domain
    {
        public virtual string TransactionId { get; set; }

        public virtual string ClientIdentifier { get; set; }

        public virtual string UserId { get; set; }
        
        public virtual string Ticker { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual decimal Units { get; set; }

        public virtual decimal PricePerUnit { get; set; }

        public virtual decimal Fees { get; set; }

        public virtual AssetTypeEnum AssetType { get; set; }
        
        public virtual TransactionTypeEnum TransactionType { get; set; }

        protected Transaction()
        {
        }

        public Transaction(TransactionDto dto)
        {
            TransactionId = dto.TransactionId;
        }
    }
}