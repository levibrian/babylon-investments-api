using System;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Abstractions.Requests;

namespace Babylon.Investments.Domain.Objects.Base
{
    public abstract class Transaction : Abstractions.Objects.Domain
    {
        public string TransactionId => TransactionRequest.TransactionId ?? Guid.NewGuid().ToString();

        public string TenantId => TransactionRequest.TenantId;
        
        public string UserId => TransactionRequest.UserId;

        public TransactionTypeEnum TransactionType => TransactionRequest.TransactionType;
        
        public string Ticker => TransactionRequest.Ticker;
        
        public DateTime Date => TransactionRequest.Date;
        
        public decimal Units => TransactionRequest.Units;
        
        public decimal PricePerUnit => TransactionRequest.PricePerUnit;
        
        public decimal Fees => TransactionRequest.Fees;
        
        public decimal AveragePricePerUnit { get; set; }
        
        public decimal PreviousUnits { get; set; }
        
        public decimal TransactedUnits { get; set; }

        public decimal CumulativeUnits { get; set; }

        public decimal PreviousValue { get; set; }
        
        public decimal TransactedValue { get; set; }

        public decimal CumulativeValue { get; set; }

        protected readonly TransactionPostRequest TransactionRequest;
        
        protected Transaction(TransactionPostRequest transactionRequest)
        {
            TransactionRequest = transactionRequest ?? throw new ArgumentNullException(nameof(transactionRequest));
        }
    }
}