using tupapi.Shared.Enums;

namespace tupapi.Shared.Interfaces
{
    public interface IResponse<T>
    {
        ApiResult ApiResult { get; set; }
        T Data { get; set; }
    }
}