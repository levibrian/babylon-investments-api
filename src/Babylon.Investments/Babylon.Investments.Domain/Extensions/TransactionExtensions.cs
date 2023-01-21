using System.Collections.Generic;
using System.Linq;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Objects.Base;

namespace Babylon.Investments.Domain.Extensions
{
    public static class TransactionExtensions
    {
        public static decimal GetNetUnits(this IEnumerable<Transaction> transactionHistory)
        {
            var unitsFromBuyOperations = transactionHistory
                .Where(x => x.TransactionType == TransactionTypeEnum.Buy)
                .Sum(x => x.Units);
            
            var unitsFromSellOperations = transactionHistory
                .Where(x => x.TransactionType == TransactionTypeEnum.Sell)
                .Sum(x => x.Units);
            
            return unitsFromBuyOperations - unitsFromSellOperations;
        }

        public static IEnumerable<Transaction> GetNetPositions(this IEnumerable<Transaction> transactionHistory)
        {
            var orderedHistory = transactionHistory
                .Where(x => x.TransactionType != TransactionTypeEnum.Dividend)
                .OrderByDescending(x => x.Date);

            return orderedHistory;
        }
    }
}