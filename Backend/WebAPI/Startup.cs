using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //1)APÝ CORS ÝMPLEMENTASYON KODU BAÞLANGIÇ 
            services.AddCors();
                /*services.AddCors(options =>
                {
                    options.AddPolicy("AllowOrigin",
                        builder => builder.WithOrigins("http://localhost:3000"));
                });*/
            //1)APÝ CORS ÝMPLEMENTASYON KODU BÝTÝÞ

            //1.) JWT ÝMPLEMENTASYONU BAÞLANGIC
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters=new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            //1.) JWT ÝMPLEMENTASYONU BÝTÝÞ
            
            //Kendi yazdýðýmýz servis baðýmlýlýklarýný yükleme iþlemi kodu baþlangic
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule(),
            });
            //Kendi yazdýðýmýz servis baðýmlýlýklarýný yükleme iþlemi kodu baþlangic

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Middleware implementasyonu 
            app.ConfigureCustomExceptionMiddleware();
            //Middleware implementasyonu 

            // 2)APÝ CORS ÝMPLEMENTASYON KODU BAÞLANGIÇ
            app.UseCors(builder => builder.WithOrigins("http://localhost:44335").AllowAnyHeader()); // buraya belirtmezsen siteye veri gitmez
            // 2)APÝ CORS ÝMPLEMENTASYON KODU BÝTÝÞ

            app.UseHttpsRedirection();

            app.UseRouting();
            //2.) JWT ÝMPLEMENTASYONU BAÞLANGIC
            app.UseAuthentication();
            app.UseAuthorization();
            //2.) JWT ÝMPLEMENTASYONU BÝTÝÞ
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
