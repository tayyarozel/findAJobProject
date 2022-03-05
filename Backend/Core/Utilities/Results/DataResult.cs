using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        //bunlar Resulttan geliyor
        //burada hepsi olacak veri,mesaj, durum ama bunu kullanan aynı zamanda aşağıdakinide çalıştırsın
        public DataResult(T data,bool success, string message) : base(success, message)
        {
            Data = data;
        }

        //mesajsız olan constructor
        public DataResult(T data, bool success):base(success)
        {
            Data = data;
        }

        //burası IDatResult'tan geliyor
        public T Data { get; }
    }
}
