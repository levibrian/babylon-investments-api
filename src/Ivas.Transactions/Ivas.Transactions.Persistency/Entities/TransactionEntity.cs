using System;
using Amazon.DynamoDBv2.DataModel;

namespace Ivas.Transactions.Persistency.Entities
{
    public class TransactionEntity
    {
        [DynamoDBHashKey] 
        public string TransactionId { get; set; }
        
        [DynamoDBRangeKey]
        public string ClientIdentifier { get; set; }

        public string UserId { get; set; }
        
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public string TransactionType { get; set; }
    }
}