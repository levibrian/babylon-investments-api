using System;
using System.Collections.Generic;
using System.Linq;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Objects.Base;

namespace Babylon.Investments.Domain.Objects
{
    public class PositionSummary
    {
        public string UserId => _transactions?.FirstOrDefault()?.UserId;

        public string Ticker => _transactions?.FirstOrDefault()?.Ticker;

        public decimal Shares => BuyPositions.Sum(x => x.Units) - 
                                 SellPositions.Sum(x => x.Units);

        public decimal TotalInvested => this.Shares * this.PricePerShare;

        public decimal RealizedDividends => CalculateRealizedDividends();

        public decimal RealizedGainLoss => CalculateRealizedGains();

        public decimal PricePerShare => BuyPositions?
            .Average(x => x.PricePerUnit) ?? default;

        public PositionSummary(IEnumerable<Transaction> transactions)
        {
            _transactions = transactions 
                            ?? throw new ArgumentNullException(nameof(transactions));
        }

        private readonly IEnumerable<Transaction> _transactions;

        private IEnumerable<Transaction> BuyPositions => _transactions
            .Where(x => 
                x.TransactionType == TransactionTypeEnum.Buy);

        private IEnumerable<Transaction> SellPositions => _transactions
            .Where(x =>
                x.TransactionType == TransactionTypeEnum.Sell);
        
        private IEnumerable<Transaction> DividendPositions => _transactions
            .Where(x =>
                x.TransactionType == TransactionTypeEnum.Dividend);
        
        private decimal CalculateRealizedDividends()
        {
            return DividendPositions
                .Sum(dividendPosition => 
                    dividendPosition.Units * dividendPosition.PricePerUnit);
        }
        
        private decimal CalculateRealizedGains()
        {
            return (decimal) 0.00;
        }
    }
}