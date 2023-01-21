using System;
using Babylon.Investments.Domain.Abstractions.Enums;

namespace Babylon.Investments.Domain.Objects.Interfaces
{
    public interface IOperation
    {
        string TransactionId { get; }

        string TenantId { get; }

        string UserId { get; }
        
        TransactionTypeEnum TransactionType { get; }
        
        string Ticker { get; }

        DateTime Date { get; }

        decimal Units { get; }

        decimal PricePerUnit { get; }

        decimal Fees { get; }

        decimal PreviousUnits { get; }
        
        decimal CumulativeUnits { get; } 
        
        decimal TransactedValue { get; }

        decimal PreviousValue { get; }

        decimal CumulativeValue { get; }
    }
}