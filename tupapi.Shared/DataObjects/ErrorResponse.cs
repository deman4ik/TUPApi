using System;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;

namespace tupapi.Shared.DataObjects
{
    public class ErrorResponse : IErrorResponse
    {
        public ErrorResponse(ErrorType errorType = ErrorType.None,
            string message = null, Exception exception = null)
        {
            ErrorType = errorType;
            Message = message;
            Exception = exception;

        }

        public ErrorType ErrorType { get; }
        public Exception Exception { get; }
        public string Message { get; }

        public override string ToString()
        {
            return "# Error Type:" + Environment.NewLine + ErrorType.ToString() + Environment.NewLine +
                   "# Response Message:" + Environment.NewLine + Message;
        }
    }
}