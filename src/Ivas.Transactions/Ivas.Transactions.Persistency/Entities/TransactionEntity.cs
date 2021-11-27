using System;
using Amazon.DynamoDBv2.DataModel;

namespace Ivas.Transactions.Persistency.Entities
{
    public class TransactionEntity
    {
        [DynamoDBHashKey]
        public long UserId { get; set; }
        
        [DynamoDBRangeKey] 
        public string Id { get; set; }

        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public string TransactionType { get; set; }
    }
}