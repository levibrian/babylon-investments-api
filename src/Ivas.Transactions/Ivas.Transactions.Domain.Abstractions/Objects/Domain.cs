using System.Collections.Generic;

namespace Ivas.Transactions.Domain.Abstractions.Objects
{
    public abstract class Domain
    {
        public ICollection<string> DomainErrors { get; set; }

        protected Domain()
        {
            DomainErrors = new List<string>();
        }
    }
}