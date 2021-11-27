using System;
using Ivas.Transactions.Domain.Dtos;

namespace Ivas.Transactions.Domain.Objects
{
    public class TransactionCreate : Transaction
    {
        public TransactionCreate(TransactionCreateDto transactionCreateDto)
        {
            if (transactionCreateDto == null)
                throw new ArgumentNullException(nameof(transactionCreateDto));

            Id = Guid
                .NewGuid()
                .ToString();

            UserId = transactionCreateDto.UserId;
            
            Ticker = transactionCreateDto.Ticker.ToUpperInvariant();
            
            Date = Date == new DateTime() 
                ? DateTime.UtcNow 
                : transactionCreateDto.Date;
            
            Units = transactionCreateDto.Units;
            
            PricePerUnit = transactionCreateDto.PricePerUnit;
            
            TransactionType = transactionCreateDto.TransactionType;
        }
    }
}