using System;
using System.Collections.Generic;
using System.Linq;

namespace Babylon.Transactions.Shared.Notifications
{
    public interface IResult
    {
        bool IsSuccess { get; }

        bool IsFailure { get; }

        IEnumerable<Error> Errors { get; }

        Result AddErrors(IEnumerable<Error> errors);

        Result AddError(Error error);
    }

    public class Result : IResult
    {
        public IEnumerable<Error> Errors { get; } = new List<Error>();

        public bool IsSuccess => !Errors.Any();

        public bool IsFailure => !IsSuccess;

        public object Value { get; set; }
        
        private static Result Empty() => new Result();
        
        public static Result Ok() => Empty();

        public static Result Ok(object objectValue) => Empty().SetValue(objectValue);

        public static Result Failure(int code, string message)
            => Failure(Error.CreateError(code, message));
        
        public static Result Failure(Error error) => Empty().AddError(error);

        public static Result Failure(IEnumerable<Error> errors) => Empty().AddErrors(errors);

        public static Result Failure(Enum error)
           => Failure(Error.CreateError(error));

        public static Result Failure(Enum error, Exception ex)
            => Failure(Error.CreateError(error, ex));
        
        public Result AddErrors(IEnumerable<Error> errors)
        {
            var localErrors =
                errors?.Where(e => e != null).ToList()
                ?? Enumerable.Empty<Error>().ToList();
            
            (Errors as List<Error>)?.AddRange(localErrors);

            return this;
        }

        public Result AddError(Error error)
        {
            if (error == null)
                return this;

            AddErrors(new List<Error>() {error});

            return this;
        }

        public Result SetValue(object value)
        {
            if (value == null) return this;

            Value = value;

            return this;
        }
    }
}