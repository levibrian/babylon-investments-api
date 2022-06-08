using System;
using Babylon.Investments.Domain.Contracts.Enums;

namespace Babylon.Investments.Domain.Contracts.Requests
{
    public class TransactionPostRequest : TransactionBaseRequest
    {
        public string UserId { get; set; }

        public string Ticker { get; set; }
        
        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal Fees { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }
    }
}