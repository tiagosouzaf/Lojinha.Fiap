using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Lojinha.Fiap.InfraStructre.Storage;
using Lojinha.Fiap.InfraStructre.Redis;
using Lojinha.Fiap.Core.Services;
using AutoMapper;
using Lojinha.Fiap.InfraStructre.Mappings;

namespace Lojinha.Fiap
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

            services.ConfigureApplicationCookie(op =>
            {
                op.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            .AddAzureAD(op => Configuration.Bind("AzureAD", op));

            services.AddScoped<IAzureStorage, AzureStorage>();
            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddScoped<IProdutoServices, ProdutoServices>();
            services.AddScoped<ICarrinhoServices, CarrinhoServices>();

            Mapper.Initialize(options => options.AddProfile<ProdutoProfile>());

            services.AddAutoMapper();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Produtos}/{action=Lista}/{id?}");
            });
        }
    }
}
