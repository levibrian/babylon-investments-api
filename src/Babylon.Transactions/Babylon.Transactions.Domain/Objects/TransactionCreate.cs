using System;
using Babylon.Networking.Interfaces.Brokers;
using Babylon.Transactions.Domain.Dtos;
using Babylon.Transactions.Domain.Enums;

namespace Babylon.Transactions.Domain.Objects
{
    public class TransactionCreate : Transaction
    {
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

        private readonly TransactionPostDto _transactionRequest;

        private readonly IFinancialsBroker _financialsBroker;
        
        public TransactionCreate()
        {
        }
        
        public TransactionCreate(
            TransactionPostDto transactionCreateDto, 
            IFinancialsBroker financialsBroker)
        {
            _transactionRequest = transactionCreateDto ?? throw new ArgumentNullException(nameof(transactionCreateDto));
            _financialsBroker = financialsBroker ?? throw new ArgumentNullException(nameof(financialsBroker));

            if (string.IsNullOrEmpty(_transactionRequest.TransactionId)) _transactionRequest.TransactionId = Guid.NewGuid().ToString();
        }
    }
}