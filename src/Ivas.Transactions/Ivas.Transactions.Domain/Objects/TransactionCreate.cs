using System;
using Ivas.Transactions.Domain.Dtos;
using Ivas.Transactions.Domain.Enums;

namespace Ivas.Transactions.Domain.Objects
{
    public class TransactionCreate : Transaction
    {
        public new string TransactionId => string.IsNullOrEmpty(_transactionRequest.TransactionId)
            ? Guid.NewGuid().ToString()
            : _transactionRequest.TransactionId;

        public new long UserId => _transactionRequest.UserId;

        public new string Ticker => _transactionRequest.Ticker;

        public new DateTime Date => _transactionRequest.Date == new DateTime()
            ? DateTime.Now
            : _transactionRequest.Date;

        public new decimal Units => _transactionRequest.Units;

        public new decimal PricePerUnit => _transactionRequest.PricePerUnit;

        public new TransactionTypeEnum TransactionType => _transactionRequest.TransactionType;
        
        private readonly TransactionCreateDto _transactionRequest;
        
        public TransactionCreate(TransactionCreateDto transactionCreateDto)
        {
            _transactionRequest = transactionCreateDto ?? throw new ArgumentNullException(nameof(transactionCreateDto));
        }
    }
}