using System;
using System.Text;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;

namespace tupapiService.Test.Infrastructure
{
    public class TestResult<T>
    {
        public string StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public ApiResult ApiResult { get; set; }
        public T Data { get; set; }
        public ErrorResponse Error { get; set; }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("# Status Code:" + Environment.NewLine + StatusCode);
            builder.AppendLine("# Is Success Status Code:" + Environment.NewLine + IsSuccessStatusCode);
            builder.AppendLine("# Api Result:" + Environment.NewLine + ApiResult);
            if (Data != null)
                builder.AppendLine("# Data:" + Environment.NewLine + Data.ToString());
            if (Error != null)
                builder.AppendLine("# Error:" + Environment.NewLine + Error.ToString());
            return builder.ToString();
        }
    }
}