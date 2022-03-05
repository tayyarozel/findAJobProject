using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
       

        //bu classı json formatına cevir
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    // bu tamamen backend'ciyi yanıltmamk için yukardakiler zaten olacak ama bu validation hatası olursa buda olsu
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
