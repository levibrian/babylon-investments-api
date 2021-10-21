using System;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsTransactionDateNotFuture : ISpecification<TransactionCreate>
    {
        public bool IsSatisfiedBy(TransactionCreate entityToEvaluate)
        {
            return entityToEvaluate.Date > DateTime.UtcNow;
        }
    }
}