using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    // Burası her porjemızde kullanabileceğimiz bağımlılıklarımızı tanımlayıp yuklenmesibi sağlıyoruz
    public class CoreModule:ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            // Cachleme İçin Başlangıç
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            // Cachleme İçin Bitiş

            //birisi senden IHttpContextAccessor isterse ona HttpContextAccessor ver
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
