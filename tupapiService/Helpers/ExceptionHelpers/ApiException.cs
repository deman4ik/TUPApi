using System;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;

namespace tupapiService.Helpers.ExceptionHelpers
{
    public class ApiException : Exception, IErrorResponse
    {
        public ApiException(ApiResult apiResult, ErrorType verr = ErrorType.None, string msg = null) : base(msg)
        {
            ApiResult = apiResult;
            ErrorType = verr;
            Exception = this;
        }

        public ApiException(ApiResult apiResult, ErrorType verr = ErrorType.None, string msg = null,
            Exception innerException = null) : base(msg, innerException)
        {
            ApiResult = apiResult;
            ErrorType = verr;
            if (innerException != null)
                InnerExceptionMessage = innerException.Message;
            Exception = this;
        }

        public ApiResult ApiResult { get; }
        public ErrorType ErrorType { get; set; }
        public string InnerExceptionMessage { get; }
        public Exception Exception { get; }

        public override string ToString()
        {
            return "ApiResult: " + ApiResult + ", " + "ErrorType: " + ErrorType + ", " + "InnerExceptionMessage: " +
                   InnerExceptionMessage;
        }
    }
}