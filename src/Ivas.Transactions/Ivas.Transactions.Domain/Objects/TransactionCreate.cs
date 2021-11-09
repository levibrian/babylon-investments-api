using System;
using System.Collections.Generic;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Domain.Abstractions.Enums;
using Ivas.Transactions.Domain.Rules;
using Ivas.Transactions.Shared.Abstractions.Specifications;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;
using Ivas.Transactions.Shared.Extensions;

namespace Ivas.Transactions.Domain.Objects
{
    public class TransactionCreate : Transaction
    {
        public TransactionCreate(TransactionCreateDto transactionCreateDto)
        {
            if (transactionCreateDto == null)
                throw new ArgumentNullException(nameof(transactionCreateDto));

            Ticker = transactionCreateDto.Ticker;
            Date = Date == new DateTime() 
                ? DateTime.UtcNow 
                : transactionCreateDto.Date;
            Units = transactionCreateDto.Units;
            PricePerUnit = transactionCreateDto.PricePerUnit;
            TransactionType = transactionCreateDto.TransactionType;
            
            Validate();
        }

        public void Validate()
        {
            var transactionRules = new IsTickerProvided()
                    .And(new IsDateNotFuture())
                    .And(new AreUnitsPositive())
                    .And(new IsPricePositive());

            transactionRules.IsSatisfiedBy(this);
        }
    }
}