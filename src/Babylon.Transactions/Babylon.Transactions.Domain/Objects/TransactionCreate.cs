using System;
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

        public override AssetTypeEnum AssetType => _transactionRequest.AssetType;
        
        public override TransactionTypeEnum TransactionType => _transactionRequest.TransactionType;
        
        
        private readonly TransactionPostDto _transactionRequest;

        public TransactionCreate()
        {
        }
        
        public TransactionCreate(TransactionPostDto transactionCreateDto)
        {
            _transactionRequest = transactionCreateDto ?? throw new ArgumentNullException(nameof(transactionCreateDto));

            if (string.IsNullOrEmpty(_transactionRequest.TransactionId)) _transactionRequest.TransactionId = Guid.NewGuid().ToString();
        }
    }
}