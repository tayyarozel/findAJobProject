using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    // burası bu projede yer alan butun methodları TRY-CATCH İÇİNE ALIYOR
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //INVOKE: HERYERDE ÇALISAN DEMEK
        public async Task InvokeAsync(HttpContext httpContext)
        {
            //BUTUN METHODLARI TRY CACHE ALIYOR
            try
            {
                //PROBLEM YOKSA DEVAM ET
                await _next(httpContext);
            }
            catch (Exception e)
            {
                //PROBLEM OLURSA HANDLE ET
                await HandleExceptionAsync(httpContext,e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json"; // tarayıcıya ben sana json formatında veri yollayacam
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;//hata kodu bunu verdik

            string message = "Internal Server Error"; // mesaj olarak bunu verdik
            IEnumerable<ValidationFailure> errors;// hataları yakalayıp kendim için liste oluşturuorum
            
            // eğer hata validation hatası ise ilgili validationdan hatayı al messaji ona cevir
            if (e.GetType()==typeof(ValidationException))
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400; //ilk basta 500 hatası verdiydim ama artık validation hatası olduğunu bildiğim için 400 yaptım
                //ve döndür
                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = message,//sistemle ilgili mesaj verme
                    Errors= errors // normalde buraları kullancııya göstertme apiden yollama boş gönder
                }.ToString());
            }

            //EĞER VALİDASYON DEĞİLDE SİSTEMSEL HATA İSE BUNU döndür
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message // normalde buraları kullancııya göstertme apiden yollama boş gönder
            }.ToString());
        }
    }
}
