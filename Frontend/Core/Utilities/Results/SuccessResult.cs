using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        // base => Resultın Constructorları 

        // ya message ver ya da işlem sonucu ile beraber mesaj ver
        public SuccessResult(string message) : base(true, message)
        {
        }

        //sadece işlem sonucu ver, mesaj VERMEEE
        public SuccessResult() : base(true)
        {
        }
    }
}
