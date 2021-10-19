using Ivas.Entities.Base;
using System;

namespace Ivas.Entities.Models
{
    public class HoldingPosition : Entity
    {
        public decimal Units { get; set; }
        public decimal PricePerShare { get; set; }
        public DateTime Date { get; set; }
    }
}
