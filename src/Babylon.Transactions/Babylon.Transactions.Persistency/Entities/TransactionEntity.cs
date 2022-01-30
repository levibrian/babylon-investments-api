using System;
using Amazon.DynamoDBv2.DataModel;

namespace Babylon.Transactions.Persistency.Entities
{
    public class TransactionEntity
    {
        [DynamoDBHashKey]
        public string ClientIdentifier { get; set; }
        
        [DynamoDBRangeKey]
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