using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupapi.Shared.Enums;

namespace tupapi.Shared.Interfaces
{
    public interface IResponse<T>
    {
        ApiResult ApiResult { get; set; }
        T Data { get; set; }
    }
}
