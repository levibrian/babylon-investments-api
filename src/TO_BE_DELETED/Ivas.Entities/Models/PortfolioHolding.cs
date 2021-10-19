using Ivas.Entities.Base;
using System.Collections.Generic;

namespace Ivas.Entities.Models
{
    public class PortfolioHolding : Entity
    {
        public virtual Security Holding { get; set; }
        public virtual ICollection<HoldingPosition> Positions { get; set; }
    }
}
