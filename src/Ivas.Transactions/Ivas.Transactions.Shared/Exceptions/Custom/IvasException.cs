using System;

namespace Ivas.Transactions.Shared.Exceptions.Custom
{
    public class IvasException : Exception
    {
        public IvasException(string message)
            : base(message)
        {

        }
    }
}
