using System;
using tupapi.Shared.Enums;

namespace tupapi.Shared.Interfaces
{
    public interface IErrorResponse
    {
        ErrorType ErrorType { get; }
        Exception Exception { get; }
        string Message { get; }

    }
}