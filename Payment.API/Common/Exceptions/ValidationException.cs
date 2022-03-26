using Payment.Domain.Exceptions;
using System.Collections.Generic;

namespace Payment.API.Common.Exceptions
{
    public sealed class ValidationException : ApplicationException
    {
        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Validation Failure", "One or more validation errors occurred")
        {
            ErrorsDictionary = errorsDictionary;
        }

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
