using System;
using System.Collections.Generic;
using System.Linq;
using Babylon.Investments.Shared.Extensions;

namespace Babylon.Investments.Shared.Notifications
{
    public class Error
    {
        public int Code { get; private set; }
        
        public string Message { get; private set; }
        
        public IList<Error> Errors => new List<Error>();

        public Exception Exception { get; private set; }

        public DateTimeOffset Created { get; private set; }
        
        public Error()
        {
            Created = DateTimeOffset.UtcNow;
        }

        public static Error Empty() => new Error();

        public static Error CopyError(Error source) => 
            Empty()
            .SetErrorCode(source.Code)
            .SetMessage(source.Message)
            .SetException(source.Exception);

        public static Error CreateError(int code, string message) => Empty().SetErrorCode(code).SetMessage(message);
        public static Error CreateError(Enum errorCode) => Empty().SetErrorCode(errorCode).SetMessage(errorCode.GetDescription());
        
        public static Error CreateError(Enum errorCode, string message) => Empty().SetErrorCode(errorCode).SetMessage(message);

        public static Error CreateError(Enum errorCode, Exception e) => Empty()
            .SetErrorCode(errorCode)
            .SetMessage(errorCode.GetDescription())
            .SetException(e);
        
        public Error SetErrorCode(int errorCode)
        {
            Code = errorCode;

            return this;
        }

        public Error SetErrorCode(Enum errorCode) => SetErrorCode(Convert.ToInt32(errorCode)).SetMessage(errorCode.ToString());

        public Error SetMessage(string message)
        {
            Message = message;

            return this;
        }
        
        public Error SetException(Exception e)
        {
            Exception = e;

            return this;
        }
    }
}