using System;

namespace Babylon.Transactions.Shared.Exceptions.Custom
{
    public class BabylonException : Exception
    {
        public BabylonException(string message)
            : base(message)
        {

        }
    }
}
