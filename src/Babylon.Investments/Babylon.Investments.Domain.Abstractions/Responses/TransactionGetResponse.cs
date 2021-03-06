using System;

namespace Babylon.Investments.Domain.Abstractions.Responses
{
    public class TransactionGetResponse
    {
        public string TransactionId { get; set; }

        public string UserId { get; set; }
        
        public string Ticker { get; set; }
        
        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public decimal Fees { get; set; }
        
        public string AssetType { get; set; }
        
        public string TransactionType { get; set; }
    }
}