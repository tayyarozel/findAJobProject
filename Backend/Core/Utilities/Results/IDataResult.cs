using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // Messagess ve işlem sonucu IResult'dan geliyor burası için ekstra birde data döndürmesi gerek onu yazdık
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}
