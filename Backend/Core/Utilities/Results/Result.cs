using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result:IResult
    {
        //hem işlem sonuc hem de mesaj döndürmek için constructor
        //:this(success)=> 2 parameteeli bu constructor çalışınca  aşağıdaki tek parametreli constructor'da çalışşın demek
        public Result(bool success, string message):this(success)
        {
            Message = message;
        }

        //sadece işlem sonucu döndürmek için constructor
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}
