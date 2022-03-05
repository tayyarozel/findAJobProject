using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    // bunu bizim autofac ile oluşturudğumuz IoC container modelini kendimiz oluşturuyoruz.
    //Örneğin webapide değilde Forms uygulamasında çalışacak biri forms uygulamasında productDal'a ihtiyacı varda IProductService ihtiacı var ona ulaşmak için
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
