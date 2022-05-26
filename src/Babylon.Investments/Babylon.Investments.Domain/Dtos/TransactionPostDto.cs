using System;
using Babylon.Investments.Domain.Enums;

namespace Babylon.Investments.Domain.Dtos
{
    public class TransactionPostDto : TransactionDto
    {
        public string UserId { get; set; }

        public string Ticker { get; set; }
        
        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal Fees { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public TransactionPostDto()
        {
            TransactionId ??= new Guid().ToString();
        }
    }
}