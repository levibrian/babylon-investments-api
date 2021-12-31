namespace Ivas.Transactions.Domain.Responses
{
    public class TransactionSummaryGetResponse
    {
        public long UserId { get; set; }

        public string Ticker { get; set; }
        
        public decimal Shares { get; set; }

        public decimal TotalInvested { get; set; }

        public decimal RealizedDividends { get; set; }

        public decimal PricePerShare { get; set; }
    }
}