using System;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Domain.Abstractions.Enums;
using Ivas.Transactions.Domain.Rules;
using Ivas.Transactions.Shared.Extensions;

namespace Ivas.Transactions.Domain.Objects
{
    public class TransactionCreate : Abstractions.Objects.Domain
    {
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Units { get; set; }

        public decimal PricePerUnit { get; set; }
        
        public TransactionTypeEnum TransactionType { get; set; }

        public TransactionCreate()
        {
            
        }
        
        public TransactionCreate(TransactionCreateDto transactionCreateDto)
        {
            if (transactionCreateDto == null)
                throw new ArgumentNullException(nameof(transactionCreateDto));

            Ticker = transactionCreateDto.Ticker;
            Date = transactionCreateDto.Date;
            Units = transactionCreateDto.Units;
            PricePerUnit = transactionCreateDto.PricePerUnit;
            TransactionType = transactionCreateDto.TransactionType;
            
            Validate();
        }

        public void Validate()
        {
            var transactionRules = new IsTickerProvided()
                .And(new IsTransactionDateNotFuture());

            var isTransactionValid = transactionRules.IsSatisfiedBy(this);
            
            if (!isTransactionValid)
                DomainErrors.Add(nameof(transactionRules));
        }
    }
}