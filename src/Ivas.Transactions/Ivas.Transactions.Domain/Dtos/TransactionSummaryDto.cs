namespace Ivas.Transactions.Domain.Dtos
{
    public class TransactionSummaryDto
    {
        public long UserId { get; set; }

        public string Ticker { get; set; }
        
        public decimal Shares { get; set; }

        public decimal TotalInvested { get; set; }

        public decimal RealizedDividends { get; set; }

        public decimal PricePerShare { get; set; }
    }
}