using System;
using tupapi.Shared.Enums;

namespace tupapiService.Test.Infrastructure
{
    public class TestResult<T>
    {
        public string StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public ApiResult ApiResult { get; set; }
        public T Data { get; set; }
        public override string ToString()
        {
            return "# Status Code:" + Environment.NewLine + StatusCode + Environment.NewLine +
                   "# Is Success Status Code:" + Environment.NewLine + IsSuccessStatusCode.ToString() +
                   Environment.NewLine +
                   "# Api Result:" + Environment.NewLine + ApiResult + Environment.NewLine +
                   "# Data:" + Environment.NewLine + Data.ToString();
        }
    }
}