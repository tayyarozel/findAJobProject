using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    // burası core katmanı dahil olmak üzere ekleyeceğimiz bütün injectionları bir araya toplayıp yukleyeceğimiz yapı
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,
            ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
               module.Load(services); 
            }

            return ServiceTool.Create(services);
        }
    }
}
