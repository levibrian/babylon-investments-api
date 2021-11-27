namespace Ivas.Transactions.Domain.Dtos
{
    public class TransactionSummaryDto : TransactionDto
    {
        public decimal TotalInvested { get; set; }
        
        public decimal RealizedGainLoss { get; set; }
        
        public decimal RealizedDividends { get; set; }

        public decimal AvgPricePerUnit { get; set; }
    }
}