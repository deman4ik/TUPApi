using tupapi.Shared.Enums;

namespace tupapiService.Test.Infrastructure
{
    public class ControllerResult
    {
        public string StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public ApiResult ApiResult { get; set; }
        public ErrorType ErrorType { get; set; }
        public string ResponseMessage { get; set; }
    }
}