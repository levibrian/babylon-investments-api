using System;
using Ivas.Transactions.Domain.Abstractions.Dtos;
using Ivas.Transactions.Domain.Enums;

namespace Ivas.Transactions.Domain.Dtos
{
    public class TransactionCreateDto : TransactionDto
    {
        public TransactionTypeEnum TransactionType { get; set; }
    }
}