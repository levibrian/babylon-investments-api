using System;

namespace Ivas.Common.Exceptions.Custom
{
    public class IvasException : Exception
    {
        public IvasException(string message)
            : base(message)
        {

        }
    }
}
