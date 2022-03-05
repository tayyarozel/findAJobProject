using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //temel voidler için, data döndürmeyenler için
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
