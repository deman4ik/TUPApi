using tupapi.Shared.Enums;

namespace tupapi.Shared.Interfaces
{
    public interface IBaseResponse
    {
        ApiResult ApiResult { get; }
        ErrorType ErrorType { get; }
        string Message { get; }
        string InnerExceptionMessage { get; }
    }
}