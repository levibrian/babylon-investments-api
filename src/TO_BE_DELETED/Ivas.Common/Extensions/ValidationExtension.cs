using FluentValidation.Results;
using System.Collections.Generic;
using System.Text;

namespace Ivas.Common.Extensions
{
    public static class ValidationExtension
    {
        public static string ToErrorString(this IList<ValidationFailure> validationFailures)
        {
            var stringBuilder = new StringBuilder();

            foreach (var error in validationFailures)
            {
                stringBuilder.AppendLine(error.ErrorMessage);
            }

            return stringBuilder.ToString();
        }
    }
}
