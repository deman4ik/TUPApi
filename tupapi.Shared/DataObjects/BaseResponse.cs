using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;

namespace tupapi.Shared.DataObjects
{
    public class BaseResponse : IBaseResponse
    {
        public BaseResponse(ApiResult apiResult = ApiResult.Ok, ErrorType errorType = ErrorType.None,
            string message = null, string innerExceptionMessage = null)
        {
            ApiResult = apiResult;
            ErrorType = errorType;
            Message = message;
            InnerExceptionMessage = innerExceptionMessage;
        }

        public ApiResult ApiResult { get; }
        public ErrorType ErrorType { get; }
        public string Message { get; }
        public string InnerExceptionMessage { get; }
    }
}