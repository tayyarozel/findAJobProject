using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        // base => Resultın Constructorları 

        // ya message ver ya da işlem sonucu ile beraber mesaj ver
        public ErrorResult(string message) : base(false, message)
        {
        }
        //sadece işlem sonucu ver, mesaj VERMEEE

        public ErrorResult() : base(false)
        {
        }
    }
}
