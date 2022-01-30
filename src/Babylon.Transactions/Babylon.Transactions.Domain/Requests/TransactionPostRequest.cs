using System;
using Babylon.Transactions.Domain.Enums;

namespace Babylon.Transactions.Domain.Requests
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