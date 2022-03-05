using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    //JWT İÇİN
    public class SecuredOperation:MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;// HER İSTEK İÇİN HERKESE YENİ BİR hTTPcONTECT oluşturur

        //bU OPERASYONU NEREDE KULLANACAKSAN BANA ROLLERİ VER
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');// ROLLERİ VİRGÜLLE AYIRDIĞIMIZ İÇİN BUNU ARRRAYA AT

            // eğer wep api değilde forms uygulamasında çalıaşcak birisi için  autofac ile mimarisi ile oluşturduğumu IoC container yapısını kendimiz oluşturuyoruz. Örneğin;
                //_productDal = ServiceTool.ServiceProvider.GetService<IProductService>(); => böyle dersem ona ulaşmış oluyorum bir forms uygulamasında

            _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();//

        }

        // nerde kullanıyorsan methodun önünde calıştır
        protected override void OnBefore(IInvocation invocation)
        {
            // kullanıcı röllerini al
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                //kullanıc rolleri içinde ilgili rol varsa sıkıntı yok eğer yoksa yetkin yok hatası ver
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
