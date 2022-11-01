using System;

namespace Babylon.Investments.Shared.Exceptions.Custom
{
    public class BabylonException : Exception
    {
        public BabylonException(string message)
            : base(message)
        {

        }
    }
}
