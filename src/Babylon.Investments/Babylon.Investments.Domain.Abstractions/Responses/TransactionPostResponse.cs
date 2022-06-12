namespace Babylon.Investments.Domain.Abstractions.Responses
{
    public class TransactionPostResponse
    {
        public string TransactionId { get; set; }
        
        public string TransactionType { get; set; }
        
        public string Ticker { get; set; }
        
        public string Date { get; set; }
        
        public double TransactedUnits { get; set; }
        
        public double PreviousUnits { get; set; }
        
        public double CumulativeUnits => PreviousUnits + TransactedUnits;

        public double TransactedValue { get; set; }

        public double PreviousValue { get; set; }

        public double CumulativeValue => PreviousValue + TransactedValue;
    }
}