namespace Babylon.Investments.Domain.Abstractions.Responses
{
    public class TransactionPostResponse
    {
        public string TransactionId { get; set; }
        
        public string TransactionType { get; set; }
        
        public string Ticker { get; set; }
        
        public string Date { get; set; }

        public decimal TransactedPricePerUnit { get; set; }

        public decimal AveragePricePerUnit { get; set; }
        
        public decimal PreviousUnits { get; set; }
        
        public decimal TransactedUnits { get; set; }

        public decimal CumulativeUnits { get; set; }

        public decimal PreviousValue { get; set; }
        
        public decimal TransactedValue { get; set; }

        public decimal CumulativeValue { get; set; }
    }
}