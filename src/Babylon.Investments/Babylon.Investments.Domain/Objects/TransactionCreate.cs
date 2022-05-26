using System;
using System.Collections.Generic;
using Babylon.Networking.Interfaces.Brokers;
using Babylon.Investments.Domain.Dtos;
using Babylon.Investments.Domain.Enums;

namespace Babylon.Investments.Domain.Objects
{
    public class TransactionCreate : Transaction
    {
        private readonly TransactionPostDto _transactionRequest;

        private readonly IEnumerable<Transaction> _transactionHistory;

        private readonly IFinancialsBroker _financialsBroker;
        
        public TransactionCreate()
        {
        }

        public TransactionCreate(
            TransactionPostDto transactionPostDto,
            IEnumerable<Transaction> transactionHistory)
        {
            _transactionRequest = transactionPostDto ?? throw new ArgumentNullException(nameof(transactionPostDto));
            _transactionHistory = transactionHistory ?? throw new ArgumentNullException(nameof(transactionHistory));
        }
        
        public TransactionCreate(
            TransactionPostDto transactionPostDto,
            IEnumerable<Transaction> transactionHistory,
            IFinancialsBroker financialsBroker)
        {
            _transactionRequest = transactionPostDto ?? throw new ArgumentNullException(nameof(transactionPostDto));
            _transactionHistory = transactionHistory ?? throw new ArgumentNullException(nameof(transactionHistory));
            _financialsBroker = financialsBroker ?? throw new ArgumentNullException(nameof(financialsBroker));
        }
        
        public override string TransactionId => _transactionRequest.TransactionId;

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
    }
}