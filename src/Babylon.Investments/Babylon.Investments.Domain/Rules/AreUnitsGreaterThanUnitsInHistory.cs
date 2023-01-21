using System.Collections.Generic;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Extensions;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class AreUnitsGreaterThanUnitsInHistory : IResultedSpecification<ICollection<Transaction>>
    {
        private readonly decimal _providedUnits;
        
        public AreUnitsGreaterThanUnitsInHistory(decimal providedUnits)
        {
            _providedUnits = providedUnits;
        }
        
        public Result IsSatisfiedBy(ICollection<Transaction> transactionHistory)
        {
            var netUnitsFromHistory = transactionHistory.GetNetUnits();
            
            var expression = netUnitsFromHistory >= _providedUnits;

            return expression ? 
                    Result.Ok() : 
                    Result.Failure(ErrorCodesEnum.UnitsProvidedGreaterThanStored);
        }
    }
}