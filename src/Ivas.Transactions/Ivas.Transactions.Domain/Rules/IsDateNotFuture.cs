using System;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Shared.Abstractions.Specifications.Interfaces;

namespace Ivas.Transactions.Domain.Rules
{
    public class IsDateNotFuture : ISpecification<TransactionCreate>
    {
        public bool IsSatisfiedBy(TransactionCreate entityToEvaluate)
        {
            var expression = entityToEvaluate.Date.Date == DateTime.UtcNow.Date || 
                             entityToEvaluate.Date.Date < DateTime.UtcNow.Date;

            if (!expression)
            {
                entityToEvaluate.DomainErrors.Add("Date provided is a future Date which is not valid.");
            }
            
            return expression;
        }
    }
}