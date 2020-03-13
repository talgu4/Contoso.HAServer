using Contoso.HAServer.Common;
using Contoso.HAServer.Common.Interfaces;
using Contoso.HAServer.Core;
using Contoso.HAServer.InMemory;
using Contoso.HAServer.Middleware;
using Contoso.HAServer.RateLimitService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Contoso.HAServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions()
                //AddSingleton<IRateLimitOptions, RateLimitOptions>()
                    .AddSingleton<IMemoryCache,MemoryCache>()
                    .RegisterCores()
                    .RegisterServices()
                    .RegisterMemoryCacheRateLimitCounter();

            services.Configure<RateLimitOptions>(Configuration.GetSection("RateLimitOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //register middlewares
            app.UseMiddlewares();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
