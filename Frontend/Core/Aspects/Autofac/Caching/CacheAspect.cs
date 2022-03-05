using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        //Cache 60 dakika duracak otomatik.
        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        // Methodu çalıştırmadan başında burayı çalıştır diyoruz
        public override void Intercept(IInvocation invocation)
        {
            // 
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");// Methodun namespacesini komple al ve methodun ismini al ÖRNEĞİN Business katamanında Concrete klasörü içinde ProductManagerda ise "namespace Business.Concrete.IProductService.GetAll" olarak al
            var arguments = invocation.Arguments.ToList(); // eğer  methodun parametrelerini varsa al listeye çevir
            var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})";
            if (_cacheManager.IsAdd(key))// sonra git cache böyle bir key var mı
            {
                invocation.ReturnValue = _cacheManager.Get(key);// o zaman cacheden getir
                return;
            }
            invocation.Proceed(); // cachede varsa methodu çalıştırmaya devam et
            _cacheManager.Add(key,invocation.ReturnValue,_duration); // ve veritabanından gelen verileri cache ekle 
        }
    }
}
