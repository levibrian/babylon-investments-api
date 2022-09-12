using System;
using System.Collections.Generic;
using System.Linq;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Networking.Interfaces.Brokers;

namespace Babylon.Investments.Domain.Objects
{
    public class TransactionCreate : Transaction
    {
        private readonly TransactionPostRequest _transactionRequest;

        private readonly IEnumerable<Transaction> _transactionHistory;

        public TransactionCreate(
            TransactionPostRequest transactionPostRequest,
            IEnumerable<Transaction> transactionHistory)
        {
            _transactionRequest = transactionPostRequest ?? throw new ArgumentNullException(nameof(transactionPostRequest));
            _transactionHistory = transactionHistory ?? throw new ArgumentNullException(nameof(transactionHistory));
        }

        public override string TransactionId => _transactionRequest.TransactionId ?? Guid.NewGuid().ToString();
        
        public override string TenantIdentifier => _transactionRequest.TenantIdentifier;

        public override string UserId => _transactionRequest.UserId;

        public override string Ticker => _transactionRequest.Ticker.ToUpperInvariant();

        public override DateTime Date => _transactionRequest.Date == new DateTime()
            ? DateTime.Now
            : _transactionRequest.Date;

        public override decimal Units => _transactionRequest.Units;

        public override decimal PricePerUnit => _transactionRequest.PricePerUnit;

        public override decimal Fees => _transactionRequest.Fees;

        public override TransactionTypeEnum TransactionType => _transactionRequest.TransactionType;

        // Auto-Calculated properties

        public decimal PreviousUnits => CalculateNetUnits();

        public decimal CumulativeUnits => PreviousUnits + Units; 
        
        public decimal TransactedValue => PricePerUnit * Units;

        public decimal PreviousValue => CalculatePreviousValue();

        public decimal CumulativeValue => PreviousValue + TransactedValue;

        public decimal AveragePricePerUnit => CalculateAveragePricePerUnit();

        private decimal CalculateNetUnits()
        {
            var buyPositions = _transactionHistory.Where(t => t.TransactionType == TransactionTypeEnum.Buy);
            var sellPositions = _transactionHistory.Where(t => t.TransactionType == TransactionTypeEnum.Sell);

            var netUnits = buyPositions.Sum(p => p.Units) - sellPositions.Sum(p => p.Units);

            return netUnits >= 0 ? netUnits : 0;
        }

        private decimal CalculatePreviousValue()
        {
            var netPositions =
                GetNetPositions()
                    .Where(x => x.TransactionType == TransactionTypeEnum.Buy);

            var averagePricePerUnit = CalculateAveragePricePerUnit();

            return averagePricePerUnit * netPositions.Sum(p => p.Units);
        }
        
        private decimal CalculateAveragePricePerUnit()
        {
            var netPositions = 
                GetNetPositions()
                    .Where(x => x.TransactionType == TransactionTypeEnum.Buy);

            return netPositions.Average(x => x.PricePerUnit);
        }

        private IEnumerable<Transaction> GetNetPositions()
        {
            var buyPositions = _transactionHistory.Where(t => t.TransactionType == TransactionTypeEnum.Buy);
            var sellPositions = _transactionHistory.Where(t => t.TransactionType == TransactionTypeEnum.Sell);

            return buyPositions.Except(sellPositions);
        }
    }
}