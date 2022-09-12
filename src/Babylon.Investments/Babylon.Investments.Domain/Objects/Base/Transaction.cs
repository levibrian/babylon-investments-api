using System;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Abstractions.Requests;

namespace Babylon.Investments.Domain.Objects.Base
{
    public class Transaction : Abstractions.Objects.Domain
    {
        public virtual string TransactionId { get; set; }

        public virtual string TenantIdentifier { get; set; }

        public virtual string UserId { get; set; }
        
        public virtual string Ticker { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual decimal Units { get; set; }

        public virtual decimal PricePerUnit { get; set; }

        public virtual decimal Fees { get; set; }
       
        public virtual TransactionTypeEnum TransactionType { get; set; }
    }
}