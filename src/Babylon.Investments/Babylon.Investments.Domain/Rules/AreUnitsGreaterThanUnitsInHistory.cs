using System.Collections.Generic;
using System.Linq;
using Babylon.Investments.Domain.Abstractions.Enums;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Shared.Notifications;
using Babylon.Investments.Shared.Specifications.Interfaces;

namespace Babylon.Investments.Domain.Rules
{
    public class AreUnitsGreaterThanUnitsInHistory : IResultedSpecification<IEnumerable<Transaction>>
    {
        private readonly decimal _providedUnits;
        
        public AreUnitsGreaterThanUnitsInHistory(decimal providedUnits)
        {
            _providedUnits = providedUnits;
        }
        
        public Result IsSatisfiedBy(IEnumerable<Transaction> entityToEvaluate)
        {
            var totalUnitsFromHistory = entityToEvaluate.Sum(t => t.Units);
            
            var expression = _providedUnits > totalUnitsFromHistory;

            return expression ? 
                    Result.Ok() : 
                    Result.Failure(ErrorCodesEnum.UnitsProvidedGreaterThanStored);
        }
    }
}