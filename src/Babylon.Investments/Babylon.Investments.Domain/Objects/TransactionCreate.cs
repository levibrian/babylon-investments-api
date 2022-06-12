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

        private readonly IFinancialsBroker _financialsBroker;
        
        public TransactionCreate()
        {
        }

        public TransactionCreate(
            TransactionPostRequest transactionPostDto,
            IEnumerable<Transaction> transactionHistory)
        {
            _transactionRequest = transactionPostDto ?? throw new ArgumentNullException(nameof(transactionPostDto));
            _transactionHistory = transactionHistory ?? throw new ArgumentNullException(nameof(transactionHistory));
        }
        
        public TransactionCreate(
            TransactionPostRequest transactionPostDto,
            IEnumerable<Transaction> transactionHistory,
            IFinancialsBroker financialsBroker)
        {
            _transactionRequest = transactionPostDto ?? throw new ArgumentNullException(nameof(transactionPostDto));
            _transactionHistory = transactionHistory ?? throw new ArgumentNullException(nameof(transactionHistory));
            _financialsBroker = financialsBroker ?? throw new ArgumentNullException(nameof(financialsBroker));
        }
        
        public new string TransactionId => _transactionRequest.TransactionId;

        public override string ClientIdentifier => _transactionRequest.ClientIdentifier;

        public override string UserId => _transactionRequest.UserId;

        public override string Ticker => _transactionRequest.Ticker.ToUpperInvariant();

        public override DateTime Date => _transactionRequest.Date == new DateTime()
            ? DateTime.Now
            : _transactionRequest.Date;

        public override decimal Units => _transactionRequest.Units;

        public override decimal PricePerUnit => _transactionRequest.PricePerUnit;

        public override decimal Fees => _transactionRequest.Fees;

        public override TransactionTypeEnum TransactionType => _transactionRequest.TransactionType;

        public decimal PreviousUnits => CalculateNetUnits();

        private decimal CalculateNetUnits()
        {
            var buyPositions = _transactionHistory.Where(t => t.TransactionType == TransactionTypeEnum.Buy);
            var sellPositions = _transactionHistory.Where(t => t.TransactionType == TransactionTypeEnum.Sell);

            var netUnits = buyPositions.Sum(p => p.Units) - sellPositions.Sum(p => p.Units);

            return netUnits >= 0 ? netUnits : 0;
        }
    }
}