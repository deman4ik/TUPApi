using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupapi.Shared.Enums;

namespace tupapi.Shared.DataObjects
{
    public class Response<T>
    {
        public Response()
        {
            ApiResult = ApiResult.Ok;
        }

        public Response(ApiResult apiResult, T data, ErrorResponse error = null)
        {
            ApiResult = apiResult;
            Data = data;
            Error = error;
        }

        public ApiResult ApiResult { get; set; }
        public T Data { get; set; }
        public ErrorResponse Error { get; set; }

        public override string ToString()
        {
            if (Data != null)
                return "# Api Result:" + Environment.NewLine + ApiResult.ToString() + Environment.NewLine +
                       Data.ToString();
            if (Error != null)
                return "# Api Result:" + Environment.NewLine + ApiResult.ToString() + Environment.NewLine +
                       Error.ToString();
            return "# Api Result:" + Environment.NewLine + ApiResult.ToString();
        }
    }
}
