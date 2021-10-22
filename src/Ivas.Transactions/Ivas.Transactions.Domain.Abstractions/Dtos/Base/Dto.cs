using System.Collections.Generic;

namespace Ivas.Transactions.Domain.Abstractions.Dtos.Base
{
    public abstract class Dto
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
